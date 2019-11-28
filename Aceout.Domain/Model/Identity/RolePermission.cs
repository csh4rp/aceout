using System.Collections.Generic;

namespace Aceout.Domain.Model.Identity
{
    public class RolePermission
    {
        public virtual int RoleId { get; set; }
        public virtual string Permission { get; set; }

        public override bool Equals(object obj)
        {
            var rolePermission = obj as RolePermission;

            return rolePermission != null && rolePermission.RoleId == RoleId &&
                rolePermission.Permission == Permission;
        }

        public override int GetHashCode()
        {
            var hashCode = -321986466;
            hashCode = hashCode * -1521134295 + RoleId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Permission);
            return hashCode;
        }
    }
}
