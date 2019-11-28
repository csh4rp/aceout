using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public interface IRolePermissionStore<TRole>
    {
        Task CreateAsync(TRole role, string permission, CancellationToken cancellationToken);
        Task<IList<string>> GetPermissionsAsync(TRole role, CancellationToken cancellationToken);
        Task<IList<string>> GetPermissionsAsync(IEnumerable<string> roleNames, CancellationToken cancellationToken);
        Task<bool> HasPermissionAsync(TRole role, string permission, CancellationToken cancellationToken);
        Task RemovePermissionAsync(TRole role, string permission, CancellationToken cancellationToken);
        Task UpdatePermissionsAsync(TRole role, IEnumerable<string> permissions, CancellationToken cancellationToken);
    }
}
