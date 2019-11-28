using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;
using Aceout.Domain.Services.Lms.Access.Exceptions;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace Aceout.Tests.Domain.Services.Lms.Access
{
    public class CourseStartAccessTests
    {
        private IUserLessonRepository repoMock;
        private ILmsAccessService service;
        private LessonAccessInfo accessInfo;

        public static object[][] ValidGroupDates;
        public static object[][] InvalidGroupDates;

        static CourseStartAccessTests()
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

        public CourseStartAccessTests()
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
        public void CheckCourseStartAccess_ThrowsAccessDenied_CourseAccessInfoNull()
        {
            Action action = () => service.CheckCourseStartAccessAsync((LessonAccessInfo)null).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessDeniedException>(action);
        }

        [Fact]
        public void CheckCourseStartAccess_ThrowsAccessDenied_CourseNotActive()
        {
            accessInfo.IsCourseActive = false;

            Action action = () => service.CheckCourseStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessDeniedException>(action);
        }

        [Fact]
        public void CheckCourseStartAccess_ThrowsAlreadyStarted_AlreadyStarted()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);

            Action action = () => service.CheckCourseStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAlreadyStartedException>(action);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 4)]
        public void CheckCourseStartAccess_ThrowsAttemptsReached_AttemptsReached(int limit, int attempt)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddHours(-1);
            accessInfo.GroupCourseAttemptCount = limit;
            accessInfo.CourseAttempt = attempt;

            Action action = () => service.CheckCourseStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAttempsReachedException>(action);
        }


        [Theory, MemberData(nameof(InvalidGroupDates))]
        public void CheckCourseStartAccess_ThrowsTimeElapsed_TimeElapsed(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddHours(-1);
            accessInfo.GroupToDate = toDate;
            accessInfo.GroupFromDate = fromDate;

            Action action = () => service.CheckCourseStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessTimeElapsedException>(action);
        }

        [Fact]
        public async void CheckCourseStartAccess_ChecksValid_AccessGranted()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-2);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddDays(-1);

            await service.CheckCourseStartAccessAsync(accessInfo);
        }

        [Theory, MemberData(nameof(ValidGroupDates))]
        public async void CheckCourseStartAccess_ChecksValid_DateRestricted(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-2);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.GroupFromDate = fromDate;
            accessInfo.GroupToDate = toDate;

            await service.CheckCourseStartAccessAsync(accessInfo);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public async void CheckCourseStartAccess_ChecksValid_AttemptRestricted(int attemptsLimit, int attempt)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-2);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.GroupCourseAttemptCount = attemptsLimit;
            accessInfo.CourseAttempt = attempt;

            await service.CheckCourseStartAccessAsync(accessInfo);
        }
    }
}
