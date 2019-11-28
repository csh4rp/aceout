using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class CourseMapping : ClassMap<Course>
    {
        public CourseMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Map(x => x.CoursePathId);
            this.Map(x => x.Description).CustomSqlType("TEXT");
            this.Map(x => x.IsActive).CustomSqlType("BIT");
            this.Map(x => x.RequireLessonOrder).CustomSqlType("BIT");
            this.Map(x => x.Name);
            this.Map(x => x.PictureUrl);
            this.Map(x => x.PassThreshold);

            this.HasMany(x => x.Lessons).KeyColumn("CourseId")
                .Inverse()
                .Cascade
                .AllDeleteOrphan();
            this.HasMany(x => x.Groups).KeyColumn("CourseId")
                .Inverse()
                .Cascade
                .AllDeleteOrphan();

        }
    }
}
