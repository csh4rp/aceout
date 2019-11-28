using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Lms.Access;
using Aceout.Domain.Services.Lms.Access.Exceptions;
using Moq;
using Xunit;

namespace Aceout.Tests.Domain.Services.Lms.Access
{
    public class LessonStartAccessTests
    {
        private IUserLessonRepository repoMock;
        private ILmsAccessService service;
        private LessonAccessInfo accessInfo;

        public static object[][] ValidDates;
        public static object[][] InvalidDates;

        static LessonStartAccessTests()
        {
            ValidDates = new[]
            {
                new object[] { null, null},
                new object[] { null, DateTime.UtcNow.AddDays(1)},
                new object[] { DateTime.UtcNow.AddDays(-1), null},
                new object[] { DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1)},
            };

            InvalidDates = new[]
            {
                new object[] { null, DateTime.UtcNow.AddDays(-1)},
                new object[] { DateTime.UtcNow.AddDays(1), null},
                new object[] { DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1)},
                new object[] { DateTime.UtcNow.AddDays(2), DateTime.UtcNow.AddDays(4)},
            };
        }

        public LessonStartAccessTests()
        {
            var mock = new Mock<IUserLessonRepository>();
            mock.Setup(x => x.ArePreviousPassedAsync(It.IsAny<int>(), It.IsAny<int>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));

            repoMock = mock.Object;
            service = new LmsAccessService(repoMock);

            accessInfo = new LessonAccessInfo
            {
                LessonId = 1,
                UserId = 1,
                IsCourseActive = true,
                IsLessonActive = true
            };
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsAccesDenied_AccessDenied()
        {
            Action action = () => service.CheckLessonStartAccessAsync(null).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsAccesDenied_CourseNotActive()
        {
            accessInfo.IsCourseActive = false;

            Action action = () => service.CheckLessonStartAccessAsync(null).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsAccesDenied_LessonNotActive()
        {
            accessInfo.IsLessonActive = false;

            Action action = () => service.CheckLessonStartAccessAsync(null).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Theory, MemberData(nameof(InvalidDates))]
        public void CheckLessonStartAccess_ThrowsTimeElapsed_TimeElapsed(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.GroupFromDate = fromDate;
            accessInfo.GroupToDate = toDate;

            Action action = () => service.CheckLessonStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessTimeElapsedException>(action);
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsCourseCompleted_Completed()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-2);
            accessInfo.CourseCompletedDate = DateTime.UtcNow.AddDays(-1);

            Action action = () => service.CheckLessonStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAlreadyCompletedException>(action);
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsCourseNotStarted_NotStarted()
        {
            Action action = () => service.CheckLessonStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseNotStartedException>(action);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 4)]
        public void CheckLessonStartAccess_ThrowsAttemptsReached_AttemptsReached(int limit, int attempt)
        {
            accessInfo.LessonAttemptCount = limit;
            accessInfo.UserLessonAttempt = attempt;
            accessInfo.CourseStartedDate = DateTime.UtcNow;

            Action action = () => service.CheckLessonStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<LessonAttemptsReachedException>(action);
        }

        [Fact]
        public void CheckLessonStartAccess_ThrowsPreviousNotPassed_NotPassed()
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.RequireLessonOrder = true;
            accessInfo.Position = 2;
            accessInfo.UserCourseId = 1;

            var mock = new Mock<IUserLessonRepository>();
            mock.Setup(x => x.ArePreviousPassedAsync(It.IsAny<int>(), It.IsAny<int>(), default(CancellationToken)))
                .Returns(Task.FromResult(false));

            var accessService = new LmsAccessService(mock.Object);

            Action action = () => accessService.CheckLessonStartAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<PreviousLessonsNotPassedException>(action);
        }

        [Theory, MemberData(nameof(ValidDates))]
        public async void CheckLessonStartAccess_ChecksValid_DateRestricted(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.GroupFromDate = fromDate;
            accessInfo.GroupToDate = toDate;
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);

            await service.CheckLessonStartAccessAsync(accessInfo);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public async void CheckLessonStartAccess_ChecksValid_AttemptsRestricted(int limit, int attempt)
        {
            accessInfo.CourseStartedDate = DateTime.UtcNow.AddDays(-1);
            accessInfo.LessonAttemptCount = limit;
            accessInfo.UserLessonAttempt = attempt;

            await service.CheckLessonStartAccessAsync(accessInfo);
        }
    }
}
