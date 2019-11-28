using Aceout.Domain;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public class RoleManager : RoleManager<Role>
    {
        protected IRolePermissionStore<Role> RolePermissionStore { get; }
        protected IRoleStore RoleStore => Store as IRoleStore;
        private IUnitOfWork _unitOfWork;

        public RoleManager(IRoleStore store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger, IRolePermissionStore<Role> rolePermissionStore,
            IUnitOfWork unitOfWork) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
            RolePermissionStore = rolePermissionStore;
            _unitOfWork = unitOfWork;
        }

        public Task AddPermissionAsync(Role role, Permission permission)
        {
            return this.RolePermissionStore.CreateAsync(role, permission.ToString(), this.CancellationToken);
        }

        public Task RemovePermissionAsync(Role role, Permission permission)
        {
            return this.RolePermissionStore.RemovePermissionAsync(role, permission.ToString(), this.CancellationToken);
        }

        public async Task<IList<Permission>> GetPermissionsAsync(Role role)
        {
            var permissionNames = await this.RolePermissionStore.GetPermissionsAsync(role, this.CancellationToken);
            var permissions = new List<Permission>(permissionNames.Count);

            foreach (var name in permissionNames)
            {
                permissions.Add((Permission)Enum.Parse(typeof(Permission), name));
            }

            return permissions;
        }

        public async Task<IList<Permission>> GetPermissionsAsync(IEnumerable<string> roleNames)
        {
            var permissionNames = await this.RolePermissionStore.GetPermissionsAsync(roleNames, this.CancellationToken);
            var permissions = new List<Permission>(permissionNames.Count);

            foreach (var name in permissionNames)
            {
                permissions.Add((Permission)Enum.Parse(typeof(Permission), name));
            }

            return permissions;
        }

        public Task<bool> HasPermissionAsync(Role role, Permission permission)
        {
            return this.RolePermissionStore.HasPermissionAsync(role, permission.ToString(), this.CancellationToken);
        }

        public Task<IList<Role>> GetRolesAsync()
        {
            return RoleStore.GetRolesAsync(this.CancellationToken);
        }

        public async Task CreateAsync(Role role, IEnumerable<Permission> permissions)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var permissionNames = permissions.Select(x => x.ToString());
                await CreateAsync(role);
                await this.RolePermissionStore.UpdatePermissionsAsync(role, permissionNames, this.CancellationToken);

                await transaction.CommitAsync();
            }
        }

        public async Task UpdateAsync(Role role, IEnumerable<Permission> permissions)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var permissionNames = permissions.Select(x => x.ToString());
                await UpdateAsync(role);
                await this.RolePermissionStore.UpdatePermissionsAsync(role, permissionNames, this.CancellationToken);

                await transaction.CommitAsync();
            }
        }

        public Task DeleteAsync(int id)
        {
            return this.RoleStore.DeleteAsync(id, this.CancellationToken);
        }
    }
}
