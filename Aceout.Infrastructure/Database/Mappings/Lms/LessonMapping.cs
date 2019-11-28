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
    public class LessonMapping : ClassMap<Lesson>
    {
        public LessonMapping()
        {
            this.Id(x => x.Id).GeneratedBy.Identity();
            this.Map(x => x.IsActive).CustomSqlType("BIT");
            this.Map(x => x.Description).CustomSqlType("TEXT");
            this.Map(x => x.CourseId);
            this.Map(x => x.Name);
            this.Map(x => x.PassThreshold);
            this.Map(x => x.Position);
            this.Map(x => x.Type).CustomType<LessonType>();
            this.Map(x => x.AttemptCount);
            this.Map(x => x.AllowAnswerCheck).CustomSqlType("BIT");
            this.Map(x => x.AllowAnswerPreview).CustomSqlType("BIT");
            this.HasMany(x => x.Elements).KeyColumn("LessonId").Inverse().Cascade.AllDeleteOrphan();
            this.HasMany(x => x.UserLessons).KeyColumn("LessonId").Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
