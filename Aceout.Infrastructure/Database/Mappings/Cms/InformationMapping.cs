using Aceout.Domain.Model.Cms;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Cms
{
    public class InformationMapping : ClassMap<Information>
    {
        public InformationMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Table("Information");

            this.Map(x => x.Content).CustomSqlType("TEXT");
            this.Map(x => x.CreatedDate);
            this.Map(x => x.FromDate);
            this.Map(x => x.ToDate);
            this.Map(x => x.Title);
            this.Map(x => x.UserId);

            this.HasMany(x => x.GroupInformations).Inverse().Cascade.AllDeleteOrphan();
            this.References(x => x.User).Column("UserId").Not.Insert().Not.Update();
        }
    }
}
