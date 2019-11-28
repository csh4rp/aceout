using Aceout.Domain.Model.Organization;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Organization
{
    public class GroupMapping : ClassMap<Group>
    {
        public GroupMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Table("`Group`");
            this.Map(x => x.Language);
            this.Map(x => x.Name);

            this.HasMany(x => x.GroupCourses).KeyColumn("GroupId").Inverse().Cascade.All();
            this.HasMany(x => x.GroupUsers).KeyColumn("GroupId").Inverse().Cascade.All();
        }

    }
}
