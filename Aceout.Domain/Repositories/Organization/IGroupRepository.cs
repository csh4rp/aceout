using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Organization
{
    public interface IGroupRepository
    {
        Task AddAsync(Group group, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(Group group, CancellationToken cancellationToken = default(CancellationToken));
        Task<Group> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
