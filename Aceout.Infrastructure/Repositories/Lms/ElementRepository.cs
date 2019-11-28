using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Repositories.Trainings;
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
    public class ElementRepository : IElementRepository
    {
        private readonly ISession _session;

        public ElementRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(IEnumerable<Element> elements, CancellationToken cancellationToken)
        {
            foreach (var item in elements)
            {
                _session.Save(item);
            }

            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(IEnumerable<int> records, CancellationToken cancellationToken)
        {
            return _session.Query<Element>()
                .Where(x => records.Contains(x.Id))
                .DeleteAsync(cancellationToken);
        }

        public async Task<IEnumerable<Element>> GetForLessonAsync(int lessonId, CancellationToken cancellationToken)
        {
            return await _session.Query<Element>()
                .Where(x => x.LessonId == lessonId)
                .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(IEnumerable<Element> records, CancellationToken cancellationToken)
        {
            foreach (var item in records) _session.Update(item);

            return _session.FlushAsync(cancellationToken);
        }
    }
}
