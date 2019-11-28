using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;
using Aceout.Domain.Services.Lms.Access.Exceptions;
using Moq;
using Xunit;

namespace Aceout.Tests.Domain.Services.Lms.Access
{
    public class CourseFinishAccessTests
    {
        private IUserLessonRepository repoMock;
        private ILmsAccessService service;
        private LessonAccessInfo accessInfo;

        public static object[][] ValidGroupDates;
        public static object[][] InvalidGroupDates;

        static CourseFinishAccessTests()
        {
            ValidGroupDates = new[]
            {
                new object[] { null, null},
                new object[] { null, DateTime.UtcNow.AddDays(1)},
                new object[] { DateTime.UtcNow.AddDays(-1), null},
                new object[] { DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1)},
            };

            InvalidGroupDates = new[]
            {
                new object[] { null, DateTime.UtcNow.AddDays(-1)},
                new object[] { DateTime.UtcNow.AddDays(1), null},
                new object[] { DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1)},
                new object[] { DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(4)},
            };
        }

        public CourseFinishAccessTests()
        {
            repoMock = Mock.Of<IUserLessonRepository>();
            service = new LmsAccessService(repoMock);

            accessInfo = new LessonAccessInfo
            {
                UserId = 1,
                CourseId = 1,
                IsCourseActive = true,
                GroupCourseAttemptCount = 1,
            };
        }

        [Fact]
        public void CheckCourseFinishAccess_ThrowsNotStarted_AccessDenied()
        {
            Action action = () => service.CheckCourseFinishAccessAsync(null).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessDeniedException>(action);
        }

        [Fact]
        public void CheckCourseFinishAccess_ThrowsNotStarted_CourseNotActive()
        {
            accessInfo.IsCourseActive = false;

            Action action = () => service.CheckCourseFinishAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessDeniedException>(action);
        }

        [Fact]
        public void CheckCourseFinishAccess_ThrowsNotStarted_NotStared()
        {
            Action action = () => service.CheckCourseFinishAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseNotStartedException>(action);
        }

        [Fact]
        public void CheckCourseFinishAccess_ThrowsAlreadyFinished_AlreadyFinished()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.CourseCompletedDate = DateTime.UtcNow;;

            Action action = () => service.CheckCourseFinishAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAlreadyCompletedException>(action);
        }

        [Theory, MemberData(nameof(InvalidGroupDates))]
        public void CheckCourseFinishAccess_ThrowsAccessTimeElapsed_TimeElapsed(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.GroupFromDate = fromDate;
            accessInfo.GroupToDate = toDate;

            Action action = () => service.CheckCourseFinishAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessTimeElapsedException>(action);
        }

        [Fact]
        public async void CheckCourseFinishAccess_ChecksValid_Started()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);

            await service.CheckCourseFinishAccessAsync(accessInfo);
        }

        [Theory, MemberData(nameof(ValidGroupDates))]
        public async void CheckCourseFinishAccess_ChecksValid_DateRestricted(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.GroupFromDate = fromDate;
            accessInfo.GroupToDate = toDate;

           await service.CheckCourseFinishAccessAsync(accessInfo);
        }
    }
}
