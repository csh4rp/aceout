using Aceout.Domain.Model.Cms;
using Aceout.Domain.Repositories.Cms;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Cms
{
    public class InformationRepository : IInformationRepository
    {
        private ISession _session;

        public InformationRepository(ISession session)
        {
            _session = session;
        }

        public Task AssAsync(Information entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(entity);
            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Query<Information>()
                .Where(x => x.Id == id)
                .Delete();

            return _session.FlushAsync(cancellationToken);
        }

        public Task<Information> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Information>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Information entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(entity);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
