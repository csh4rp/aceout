using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public interface IRoleStore : IRoleStore<Role>, IQueryableRoleStore<Role>
    {
        Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
