using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using FluentNHibernate.Mapping;
using NHibernate.Id;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class MaterialCategoryMapping : ClassMap<MaterialCategory>
    {
        public MaterialCategoryMapping()
        {
            this.Table("MaterialCategory");
            this.Id(x => x.Id).GeneratedBy.Identity();

            this.Map(x => x.Language);
            this.Map(x => x.Name);
            this.HasMany(x => x.Materials).KeyColumn("CategoryId").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
