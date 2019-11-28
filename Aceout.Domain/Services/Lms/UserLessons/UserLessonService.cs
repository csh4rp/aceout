using System.Threading;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;

namespace Aceout.Domain.Services.Lms.UserLessons
{
    public class UserLessonService : IUserLessonService
    {
        private readonly ILessonAccessInfoRepository _userLessonInfoRepository;
        private readonly IUserLessonRepository _userLessonRepository;
        private readonly IUserElementRepository _userElementRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILmsAccessService _accessService;

        public UserLessonService(ILessonAccessInfoRepository userLessonInfoRepository,
            IUserLessonRepository userLessonRepository,
            IUserElementRepository userElementRepository, 
            IUserCourseRepository userCourseRepository, 
            IUnitOfWork unitOfWork, 
            ILmsAccessService accessService)
        {
            _userLessonInfoRepository = userLessonInfoRepository;
            _userLessonRepository = userLessonRepository;
            _userElementRepository = userElementRepository;
            _userCourseRepository = userCourseRepository;
            _unitOfWork = unitOfWork;
            _accessService = accessService;
        }

        public async Task<UserLesson> FinishLessonAsync(int lessonId, int userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var accessInfo = await _userLessonInfoRepository.GetAsync(userId, lessonId, cancellationToken);
            await _accessService.CheckLessonFinishAccessAsync(accessInfo, cancellationToken);

            var userLesson = await _userLessonRepository.GetByIdAsync(accessInfo.UserLessonId.Value, cancellationToken);
            var lessonResult = await _userElementRepository.GetElementsResultAsync(userLesson.Id, cancellationToken);

            var isLessonPassed = (accessInfo.LessonPassThreshold.HasValue && lessonResult > accessInfo.LessonPassThreshold) ||
                                 !accessInfo.LessonPassThreshold.HasValue;

            userLesson.SetResult(lessonResult, isLessonPassed);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _userLessonRepository.UpdateAsync(userLesson, cancellationToken);

                var areAllLessonsPassed = await _userLessonRepository.AreAllLessonsPassedAsync(accessInfo.UserCourseId.Value, cancellationToken);

                if (areAllLessonsPassed)
                {
                    await _accessService.CheckCourseFinishAccessAsync(accessInfo, cancellationToken);

                    var userCourse = await _userCourseRepository.GetByIdAsync(accessInfo.UserCourseId.Value, cancellationToken);
                    var courseResult = await _userCourseRepository.GetLessonsResultAsync(accessInfo.UserCourseId.Value, cancellationToken);

                    var isPassed = (accessInfo.CoursePassThreshold.HasValue && accessInfo.CoursePassThreshold <= courseResult) ||
                                   !accessInfo.CoursePassThreshold.HasValue;

                    userCourse.SetResult(courseResult, isPassed);

                    await _userCourseRepository.UpdateAsync(userCourse, cancellationToken);
                }

                await transaction.CommitAsync(cancellationToken);
            }

            return userLesson;
        }

        public async Task<UserLesson> StartLessonAsync(int lessonId, int userId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var accessInfo = await _userLessonInfoRepository.GetAsync(userId, lessonId, cancellationToken);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                if (accessInfo != null && !accessInfo.UserCourseId.HasValue)
                {
                    await _accessService.CheckCourseStartAccessAsync(accessInfo, cancellationToken);

                    var userCourse = new UserCourse(userId, accessInfo.CourseId);
                    await _userCourseRepository.AddAsync(userCourse, cancellationToken);
                    accessInfo.UserCourseId = userCourse.Id;
                    accessInfo.CourseStartedDate = userCourse.StartedDate;
                }

                await _accessService.CheckLessonStartAccessAsync(accessInfo, cancellationToken);

                var userLesson = new UserLesson(userId, lessonId, accessInfo.UserCourseId.Value);
                await _userLessonRepository.AddAsync(userLesson, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return userLesson;
            }

        }
    }
}
