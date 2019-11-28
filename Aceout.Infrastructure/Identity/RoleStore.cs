using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Microsoft.AspNetCore.Identity;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public class RoleStore : IRoleStore
    {
        private readonly ISession _session;

        public IQueryable<Role> Roles => _session.Query<Role>();

        public RoleStore(ISession session)
        {
            _session = session;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            _session.Save(role);
            await _session.FlushAsync(cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var dbRole = _session.Load<Role>(role.Id);
            _session.Delete(dbRole);
            await _session.FlushAsync();

            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var id = int.Parse(roleId);
            return _session.QueryOver<Role>().Where(x => x.Id == id)
                .SingleOrDefaultAsync<Role>(cancellationToken);
        }

        public Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return _session.QueryOver<Role>().Where(x => x.Name == normalizedRoleName)
                .SingleOrDefaultAsync<Role>(cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            role.Name = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            _session.Update(role);
            await _session.FlushAsync();

            return IdentityResult.Success;
        }

        public async Task<IList<Role>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return await _session.Query<Role>()
                    .Fetch(x => x.RolePermissions)
                    .ToListAsync<Role>(cancellationToken);

        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var role = _session.Load<Role>(id);
            _session.Delete(role);
            return _session.FlushAsync();
        }
    }
}
