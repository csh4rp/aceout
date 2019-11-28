using Aceout.Domain;
using Aceout.Domain.Model.Organization;
using Aceout.Domain.Repositories.Organization;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Organization
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ISession _session;

        public GroupRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(Group group, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Save(group);
            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
             _session.Query<Group>()
                .Where(x => x.Id == id)
                .Delete();

            return _session.FlushAsync();
        }

        public Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _session.Query<Group>()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task UpdateAsync(Group group, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Update(group);
            return _session.FlushAsync(cancellationToken);
        }
    }
}
