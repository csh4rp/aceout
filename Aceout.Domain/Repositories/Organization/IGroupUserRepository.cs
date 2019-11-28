using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Organization
{
    public interface IGroupUserRepository
    {
        Task AddAsync(IEnumerable<GroupUser> groupUsers, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAllAsync(int groupId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
