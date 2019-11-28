using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class UserElementInfoMapping : ClassMap<UserElementInfo>
    {
        public UserElementInfoMapping()
        {
            this.Table("UserElementInfo");
            this.ReadOnly();
            this.Id(m => m.ElementId);

            this.Map(m => m.CourseId);
            this.Map(m => m.IsCourseActive).CustomSqlType("BIT");;
            this.Map(m => m.RequireLessonOrder).CustomSqlType("BIT");;
            this.Map(m => m.LessonId);
            this.Map(m => m.IsLessonActive).CustomSqlType("BIT");;
            this.Map(m => m.LessonPosition).CustomSqlType("BIT");;
            this.Map(m => m.FromDate);
            this.Map(m => m.ToDate);
            this.Map(m => m.UserId);
            this.Map(m => m.IsElementActive).CustomSqlType("BIT");;
            this.Map(m => m.ElementPosition);
            this.Map(m => m.ElementScale);
            this.Map(m => m.IsMaterialActive).CustomSqlType("BIT");;
            this.Map(m => m.MaterialType).CustomType<TrainingMaterialType>();
            this.Map(m => m.MaterialName);
            this.Map(m => m.MaterialContent);
            this.Map(m => m.MaterialDataModel);
            this.Map(m => m.MaterialAnswerModel);
            this.Map(m => m.UserAnswerModel);
            this.Map(m => m.CourseAttempt);
            this.Map(m => m.LessonAttempt);
            this.Map(m => m.LessonStartedDate);
            this.Map(m => m.LessonCompletedDate);
            this.Map(m => m.CourseStartedDate);
            this.Map(m => m.CourseCompletedDate);
            this.Map(m => m.UserLessonId);
            this.Map(x => x.AllowAnswerCheck);
        }
    }
}
