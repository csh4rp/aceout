using Aceout.Domain.Model.Organization;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Organization
{
    public class GroupUserMapping : ClassMap<GroupUser>
    {
        public GroupUserMapping()
        {
            this.CompositeId().KeyProperty(x => x.GroupId).KeyProperty(x => x.UserId);
            this.Table("GroupUser");

            this.References(x => x.Group).Column("GroupId").Not.Insert().Not.Update().Cascade.None();
            this.References(x => x.User).Column("USerId").Not.Insert().Not.Update().Cascade.None();
        }
    }
}
