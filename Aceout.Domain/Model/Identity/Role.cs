using System;
using System.Collections.Generic;
using System.Linq;

namespace Aceout.Domain.Model.Identity
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual ISet<UserRole> UserRoles { get; set; }
        public virtual ISet<RolePermission> RolePermissions { get; set; }

        public virtual IEnumerable<Permission> Permissions
        {
            get
            {
                return RolePermissions.Select(x => (Permission)Enum.Parse(typeof(Permission), x.Permission));
            }
        }


    }
}
