using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Repositories.Trainings;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Aceout.Infrastructure.Repositories.Lms
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ISession _session;

        public LessonRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(Lesson lesson, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(lesson);
            return _session.FlushAsync(cancellationToken);
        }

        public Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(lesson);
            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Query<Lesson>()
                .Where(x => x.Id == id)
                .Delete();

            return _session.FlushAsync(cancellationToken);
        }

        public Task<Lesson> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Lesson>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
