using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class UserCourseRepository : IUserCourseRepository
    {
        private readonly ISession _session;

        public UserCourseRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(UserCourse userCourse, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(userCourse);
            return _session.FlushAsync(cancellationToken);
        }

        public Task<UserCourse> GetByIdAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserCourse>()
                .FirstOrDefaultAsync(x => x.Id == userCourseId, cancellationToken);
        }

        public Task<UserCourse> GetLastAttemptAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserCourse>()
                .Where(x => x.UserId == userId &&
                       x.CourseId == courseId &&
                       x.Attempt == _session.Query<UserCourse>()
                                     .Where(u => u.CourseId == courseId &&
                                            u.UserId == userId)
                                            .Select(u => u.Attempt)
                                            .Max())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<decimal> GetLessonsResultAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserLesson>()
                .Where(x => x.UserCourseId == userCourseId &&
                       x.Attempt == _session.Query<UserLesson>()
                                   .Where(l => l.LessonId == x.LessonId &&
                                               l.UserCourseId == x.UserCourseId)
                                   .Select(l => l.Attempt)
                                   .Max())
              .SumAsync(x => x.Result ?? 0, cancellationToken);
        }

        public Task UpdateAsync(UserCourse userCourse, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(userCourse);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
