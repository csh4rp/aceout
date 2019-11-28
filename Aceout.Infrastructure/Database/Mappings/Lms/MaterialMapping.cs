using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class MaterialMapping : ClassMap<Material>
    {
        public MaterialMapping()
        {
            this.Table("Material");
            this.Id(x => x.Id).GeneratedBy.Identity();

            this.Map(x => x.Name);
            this.Map(x => x.IsActive).CustomSqlType("BIT");
            this.Map(x => x.CategoryId);
            this.Map(x => x.Content).CustomSqlType("TEXT");
            this.Map(x => x.DataModel).CustomSqlType("TEXT");
            this.Map(x => x.AnswerModel).CustomSqlType("TEXT");
            this.Map(x => x.Type).CustomType<TrainingMaterialType>();
            this.HasMany(x => x.Elements).KeyColumn("MaterialId").Inverse().Cascade.AllDeleteOrphan();
            this.References(x => x.MaterialCategory).Column("CategoryId").Not.Insert().Not.Update();

        }
    }
}
