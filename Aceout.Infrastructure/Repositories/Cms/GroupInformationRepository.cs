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
    public class GroupInformationRepository : IGroupInformationRepository
    {
        private readonly ISession _session;

        public GroupInformationRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(IEnumerable<GroupInformation> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var item in entities)
            {
                _session.Save(item);
            }

            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int informationId, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Query<GroupInformation>()
                .Where(x => x.InformationId == informationId)
                .Delete();


            return _session.FlushAsync(cancellationToken);
        }
    }
}
