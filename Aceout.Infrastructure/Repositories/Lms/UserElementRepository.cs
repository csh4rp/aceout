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
    public class UserElementRepository : IUserElementRepository
    {
        private readonly ISession _session;

        public UserElementRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(UserElement entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(entity);
            return _session.FlushAsync(cancellationToken);
        }

        public Task<UserElement> GetByUserLessonId(int userLessonId, int elementId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserElement>().Where(x => x.ElementId == elementId &&
                x.UserLessonId == userLessonId)
                .FirstOrDefaultAsync(cancellationToken);
                
        }

        public Task<decimal> GetElementsResultAsync(int userLessonId, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<UserElement>()
                .Where(x => x.UserLessonId == userLessonId)
                .SumAsync(x => x.Result ?? 0, cancellationToken);
        }

        public Task UpdateAsync(UserElement entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(entity);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
