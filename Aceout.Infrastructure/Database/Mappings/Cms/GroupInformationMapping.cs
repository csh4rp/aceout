using Aceout.Domain.Model.Cms;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Cms
{
    public class GroupInformationMapping : ClassMap<GroupInformation>
    {
        public GroupInformationMapping()
        {
            this.CompositeId().KeyProperty(x => x.GroupId).KeyProperty(x => x.InformationId);
            this.References(x => x.Group).Column("GroupId").Not.Insert().Not.Update();
            this.References(x => x.Information).Column("InformaionId").Not.Insert().Not.Update();
        }
    }
}
