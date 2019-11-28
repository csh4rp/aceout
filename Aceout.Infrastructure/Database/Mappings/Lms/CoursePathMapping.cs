using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class CoursePathMapping : ClassMap<CoursePath>
    {
        public CoursePathMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Table("CoursePath");
            this.Map(x => x.Language);
            this.Map(x => x.Name);
            this.Map(x => x.PictureUrl);
            this.Map(x => x.Description).CustomSqlType("TEXT");
            this.HasMany(x => x.Courses).KeyColumn("CoursePathId").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
