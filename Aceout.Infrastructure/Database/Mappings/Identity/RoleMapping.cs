using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Identity
{
    public class RoleMapping : ClassMap<Role>
    {
        public RoleMapping()
        { 
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Table("Role");
            this.HasMany(x => x.UserRoles).KeyColumn("RoleId").Inverse().Cascade.AllDeleteOrphan();
            this.HasMany(x => x.RolePermissions).KeyColumn("RoleId").Inverse().Cascade.AllDeleteOrphan();
            this.Map(x => x.Name);
        }
    }
}
