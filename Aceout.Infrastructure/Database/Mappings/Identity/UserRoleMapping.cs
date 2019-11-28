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
    public class UserRoleMapping : ClassMap<UserRole>
    {
        public UserRoleMapping()
        {
            this.CompositeId().KeyProperty(x => x.RoleId).KeyProperty(x => x.UserId);
            this.Table("UserRole");
            this.References(x => x.Role).Column("RoleId").Not.Insert().Not.Update().Cascade.None();
            this.References(x => x.User).Column("UserId").Not.Insert().Not.Update().Cascade.None();
        }
    }
}
