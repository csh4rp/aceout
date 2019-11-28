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
    public class ElementAccessTests
    {
        private IUserLessonRepository repoMock;
        private ILmsAccessService service;
        private UserElementInfo accessInfo;

        public static object[][] ValidDates;
        public static object[][] InvalidDates;

        static ElementAccessTests()
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

        public ElementAccessTests()
        {
            var mock = new Mock<IUserLessonRepository>();
            mock.Setup(x => x.ArePreviousPassedAsync(It.IsAny<int>(), It.IsAny<int>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));

            repoMock = mock.Object;
            service = new LmsAccessService(repoMock);

            accessInfo = new UserElementInfo
            {
                LessonId = 1,
                UserId = 1,
                IsCourseActive = true,
                IsLessonActive = true,
                CourseStartedDate = DateTime.UtcNow,
                LessonStartedDate = DateTime.UtcNow,
            };
        }

        [Fact]
        public void CheckElementAccess_ThrowsAccessDenied_AccessDenied()
        {
            Action action = () => service.CheckElementAccessAsync(null).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Fact]
        public void CheckElementAccess_ThrowsAccessDenied_CourseNotActive()
        {
            accessInfo.IsCourseActive = false;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Fact]
        public void CheckElementAccess_ThrowsAccessDenied_LessonNotActive()
        {
            accessInfo.IsLessonActive = false;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<LessonAccessDeniedException>(action);
        }

        [Theory, MemberData(nameof(InvalidDates))]
        public void CheckElementAccess_ThrowsTimeElapsed_TimeElapsed(DateTime? fromDate, DateTime? toDate)
        {
            accessInfo.FromDate = fromDate;
            accessInfo.ToDate = toDate;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAccessTimeElapsedException>(action);
        }

        [Fact]
        public void CheckElementAccess_ThrowsCourseNotStarted_CourseNotStarted()
        {
            accessInfo.CourseStartedDate = null;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseNotStartedException>(action);
        }

        [Fact]
        public void CheckElementAccess_ThrowsCourseCompleted_CourseCompleted()
        {
            accessInfo.CourseCompletedDate = DateTime.UtcNow;;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<CourseAlreadyCompletedException>(action);
        }

        [Fact]
        public void CheckElementAccess_ThrowsLessonNotStarted_LessonNotStarted()
        {
            accessInfo.LessonStartedDate = null;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<LessonNotStartedException>(action);
        }


        [Fact]
        public void CheckElementAccess_ThrowsLessonCompleted_LessonCompleted()
        {
            accessInfo.LessonCompletedDate = DateTime.UtcNow;

            Action action = () => service.CheckElementAccessAsync(accessInfo).GetAwaiter().GetResult();

            Assert.Throws<LessonAlreadyCompletedException>(action);
        }
    }
}
