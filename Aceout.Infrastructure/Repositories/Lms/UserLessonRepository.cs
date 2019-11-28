using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class UserLessonRepository : IUserLessonRepository
    {
        private readonly ISession _session;

        public UserLessonRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(UserLesson record, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(record);
            return _session.FlushAsync(cancellationToken);
        }

        public async Task<bool> AreAllLessonsPassedAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var anyNotPassed = await _session.Query<LessonAccessInfo>()
                .AnyAsync(x => x.IsLessonPassed != true, cancellationToken);

            return !anyNotPassed;
        }

        public async Task<bool> ArePreviousPassedAsync(int lessonId, int userCourseId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var isAnyNotPassed = await _session.Query<LessonAccessInfo>()
                .AnyAsync(x =>
                x.Position < _session.Query<LessonAccessInfo>()
                               .FirstOrDefault(l => l.LessonId == lessonId && l.UserCourseId == userCourseId).Position
                && x.IsLessonPassed != true
                );


            return !isAnyNotPassed;
        }

        public Task<UserLesson> GetByIdAsync(int userLessonId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserLesson>()
                .FirstOrDefaultAsync(x => x.Id == userLessonId, cancellationToken);
        }

        public Task UpdateAsync(UserLesson record, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(record);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
