using Aceout.Domain.Model.Identity;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;

namespace Aceout.Infrastructure.Database.Mappings.Identity
{
    public class RolePermissionMapping : ClassMap<RolePermission>
    {
        public RolePermissionMapping()
        {
            this.CompositeId().KeyProperty(x => x.RoleId).KeyProperty(x => x.Permission);
            this.Table("RolePermission");
        }
    }
}
