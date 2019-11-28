using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class ElementMapping : ClassMap<Element>
    {
        public ElementMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Map(x => x.IsActive).CustomSqlType("BIT");
            this.Map(x => x.LessonId);
            this.Map(x => x.MaterialId);
            this.Map(x => x.Position);
            this.Map(x => x.Scale);
            this.References(x => x.Material).Column("MaterialId").Not.Insert().Not.Update().Cascade.None();
            this.References(x => x.Lesson).Column("LessonId").Not.Insert().Not.Update().Cascade.None();
        }
    }
}
