using System.Threading;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;

namespace Aceout.Domain.Services.Lms.UserCourses
{
    public class UserCoruseService : IUserCourseService
    {
        private readonly ICourseAccessInfoRepository _accessInfoRepository;
        private readonly IGroupCourseRepository _groupCourseRepository;
        private readonly IUserCourseRepository _userCourseRepository;
        private readonly ILmsAccessService _accessService;

        public UserCoruseService(ILmsAccessService accessService,
            IUserCourseRepository userCourseRepository, 
            IGroupCourseRepository groupCourseRepository, 
            ICourseAccessInfoRepository accessInfoRepository)
        {
            _accessService = accessService;
            _userCourseRepository = userCourseRepository;
            _groupCourseRepository = groupCourseRepository;
            _accessInfoRepository = accessInfoRepository;
        }

        public async Task<UserCourse> StartCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var accessInfo = await _accessInfoRepository.GetLastestAsync(userId, courseId, cancellationToken);

            await _accessService.CheckCourseStartAccessAsync(accessInfo, cancellationToken);

            var userCourse = new UserCourse(userId, courseId);

            await _userCourseRepository.AddAsync(userCourse, cancellationToken);

            return userCourse;
        }
    }
}
