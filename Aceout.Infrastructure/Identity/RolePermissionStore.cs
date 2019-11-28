using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using NHibernate;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public class RolePermissionStore : IRolePermissionStore<Role>
    {
        private readonly ISession _session;

        public RolePermissionStore(ISession session)
        {
            _session = session;
        }

        public Task CreateAsync(Role role, string permission, CancellationToken cancellationToken)
        {
            return _session.SaveAsync(new RolePermission { RoleId = role.Id, Permission = permission }, cancellationToken);
        }

        public async Task<IList<string>> GetPermissionsAsync(Role role, CancellationToken cancellationToken)
        {
            return await _session.Query<RolePermission>()
                .Where(x => x.RoleId == role.Id)
                .Select(x => x.Permission)
                .ToListAsync<string>(cancellationToken);
        }

        public async Task<IList<string>> GetPermissionsAsync(IEnumerable<string> roleNames, CancellationToken cancellationToken)
        {
            return await _session.Query<Role>()
                .Where(x => roleNames.Contains(x.Name))
                .Join(_session.Query<RolePermission>(),
                r => r.Id,
                p => p.RoleId,
                (r, p) => p.Permission)
                .Distinct()
                .ToListAsync(cancellationToken);
        }

        public Task<bool> HasPermissionAsync(Role role, string permission, CancellationToken cancellationToken)
        {
            return _session.Query<RolePermission>()
                .Where(x => x.RoleId == role.Id &&
                x.Permission == permission)
                .Select(x => true)
                .Take(1)
                .SingleOrDefaultAsync<bool>(cancellationToken);
        }

        public async Task RemovePermissionAsync(Role role, string permission, CancellationToken cancellationToken)
        {
            var rolePermission = await _session.LoadAsync<RolePermission>(new { RoleId = role.Id, Permission = permission });
            await _session.DeleteAsync(rolePermission, cancellationToken);
        }

        public Task UpdatePermissionsAsync(Role role, IEnumerable<string> permissions, CancellationToken cancellationToken)
        {
                _session.CreateSQLQuery("DELETE FROM RolePermission WHERE RoleId = :id")
                    .SetParameter("id", role.Id)
                    .ExecuteUpdate();

                foreach (var permission in permissions)
                {
                    _session.Save(new RolePermission
                    {
                        RoleId = role.Id,
                        Permission = permission
                    });
                }

            return _session.FlushAsync();
        }
    }
}
