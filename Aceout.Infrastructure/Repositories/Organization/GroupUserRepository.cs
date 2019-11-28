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
    public class GroupUserRepository : IGroupUserRepository
    {
        private readonly ISession _session;

        public GroupUserRepository(ISession session)
        {
            _session = session;
        }

        public Task AddAsync(IEnumerable<GroupUser> groupUsers, CancellationToken cancellationToken)
        {
            foreach (var user in groupUsers)
            {
                _session.Save(user);
            }

            return _session.FlushAsync(cancellationToken);
        }

        public Task DeleteAllAsync(int groupId, CancellationToken cancellationToken)
        {
            return _session.Query<GroupUser>()
                .Where(x => x.GroupId == groupId)
                .DeleteAsync(cancellationToken);
        }
    }
}
