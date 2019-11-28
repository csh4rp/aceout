using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Lms.Access
{
    public class LmsAccessService : ILmsAccessService
    {
        private readonly IUserLessonRepository _userLessonRepository;

        public LmsAccessService(IUserLessonRepository userLessonRepository)
        {
            _userLessonRepository = userLessonRepository;
        }

        public Task CheckCourseFinishAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive)
                throw new CourseAccessDeniedException();

            if (!accessInfo.CourseStartedDate.HasValue)
                throw new CourseNotStartedException();

            if (accessInfo.CourseCompletedDate.HasValue)
                throw new CourseAlreadyCompletedException();


            var isFromDateValid = (accessInfo.GroupFromDate.HasValue && accessInfo.GroupFromDate <= DateTime.UtcNow) ||
                                  !accessInfo.GroupFromDate.HasValue;

            var isToDateValid = (accessInfo.GroupToDate.HasValue && accessInfo.GroupToDate >= DateTime.UtcNow) ||
                                !accessInfo.GroupToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();

            return Task.CompletedTask;
        }


        public Task CheckCourseStartAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive)
                throw new CourseAccessDeniedException();

            if (accessInfo.CourseStartedDate.HasValue && !accessInfo.CourseStartedDate.HasValue)
                throw new CourseAlreadyStartedException();

            if (accessInfo.CourseAttempt >= accessInfo.GroupCourseAttemptCount)
                throw new CourseAttempsReachedException();

            var isFromDateValid = (accessInfo.GroupFromDate.HasValue && accessInfo.GroupFromDate <= DateTime.UtcNow) ||
                                  !accessInfo.GroupFromDate.HasValue;

            var isToDateValid = (accessInfo.GroupToDate.HasValue && accessInfo.GroupToDate >= DateTime.UtcNow) ||
                                !accessInfo.GroupToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();

            return Task.CompletedTask;
        }


        public Task CheckElementAccessAsync(UserElementInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive || !accessInfo.IsLessonActive)
                throw new LessonAccessDeniedException();

            if(!accessInfo.CourseStartedDate.HasValue)
                throw  new CourseNotStartedException();

            if(accessInfo.CourseCompletedDate.HasValue)
                throw new CourseAlreadyCompletedException();

            if(!accessInfo.LessonStartedDate.HasValue)
                throw new LessonNotStartedException();

            if(accessInfo.LessonCompletedDate.HasValue)
                throw  new LessonAlreadyCompletedException();

            var isFromDateValid = (accessInfo.FromDate.HasValue && accessInfo.FromDate <= DateTime.UtcNow) ||
                                  !accessInfo.FromDate.HasValue;

            var isToDateValid = (accessInfo.ToDate.HasValue && accessInfo.ToDate >= DateTime.UtcNow) ||
                                !accessInfo.ToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();


            return Task.CompletedTask;
        }


        public async Task CheckLessonStartAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive || !accessInfo.IsLessonActive)
                throw new LessonAccessDeniedException();

            var isFromDateValid = (accessInfo.GroupFromDate.HasValue && accessInfo.GroupFromDate <= DateTime.UtcNow) ||
                                  !accessInfo.GroupFromDate.HasValue;

            var isToDateValid = (accessInfo.GroupToDate.HasValue && accessInfo.GroupToDate >= DateTime.UtcNow) ||
                                !accessInfo.GroupToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();

            if (accessInfo.CourseCompletedDate.HasValue)
                throw new CourseAlreadyCompletedException();

            if (!accessInfo.CourseStartedDate.HasValue)
                throw new CourseNotStartedException();

            if (accessInfo.LessonStartedDate.HasValue && !accessInfo.LessonCompletedDate.HasValue)
                throw new LessonAlreadyStartedException();

            if (accessInfo.UserLessonAttempt >= accessInfo.LessonAttemptCount)
                throw new LessonAttemptsReachedException();

            if (accessInfo.RequireLessonOrder && accessInfo.Position > 0)
            {
                var arePreviousPassed = await _userLessonRepository.ArePreviousPassedAsync(accessInfo.LessonId, accessInfo.UserCourseId.Value);

                if (!arePreviousPassed)
                    throw new PreviousLessonsNotPassedException();
            }
        }



        public Task CheckLessonFinishAccessAsync(LessonAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive || !accessInfo.IsLessonActive)
                throw new LessonAccessDeniedException();

            var isFromDateValid = (accessInfo.GroupFromDate.HasValue && accessInfo.GroupFromDate <= DateTime.UtcNow) ||
                                  !accessInfo.GroupFromDate.HasValue;
            var isToDateValid = (accessInfo.GroupToDate.HasValue && accessInfo.GroupToDate >= DateTime.UtcNow) ||
                                !accessInfo.GroupToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();

            if (accessInfo.CourseCompletedDate.HasValue)
                throw new CourseAlreadyCompletedException();

            if (!accessInfo.CourseStartedDate.HasValue)
                throw new CourseNotStartedException();

            if (!accessInfo.LessonStartedDate.HasValue)
                throw new LessonNotStartedException();

            if (accessInfo.LessonCompletedDate.HasValue)
                throw new LessonAlreadyCompletedException();

            return Task.CompletedTask;
        }

        public Task CheckCourseStartAccessAsync(CourseAccessInfo accessInfo, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (accessInfo == null || !accessInfo.IsCourseActive)
                throw new CourseAccessDeniedException();

            if (accessInfo.StartedDate.HasValue && !accessInfo.CompletedDate.HasValue)
                throw new CourseAlreadyStartedException();

            if (accessInfo.UserCourseAttempt >= accessInfo.GroupCourseAttemptCount)
                throw new CourseAttempsReachedException();

            var isFromDateValid = (accessInfo.GroupFromDate.HasValue && accessInfo.GroupFromDate <= DateTime.UtcNow) ||
                                  !accessInfo.GroupFromDate.HasValue;

            var isToDateValid = (accessInfo.GroupToDate.HasValue && accessInfo.GroupToDate >= DateTime.UtcNow) ||
                                !accessInfo.GroupToDate.HasValue;

            if (!isFromDateValid || !isToDateValid)
                throw new CourseAccessTimeElapsedException();

            return Task.CompletedTask;
        }
    }
}
