using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using FluentNHibernate.MappingModel.ClassBased;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class LessonReportMapping : ClassMap<LessonReport>
    {
        public LessonReportMapping()
        {
            this.Table("LessonReport");
            this.Id(m => m.UserLessonId);
            this.ReadOnly();

            this.Map(x => x.Attempt);
            this.Map(x => x.UserId);
            this.Map(x => x.UserName);
            this.Map(x => x.FirstName);
            this.Map(x => x.LastName);
            this.Map(x => x.Email);
            this.Map(x => x.CourseId);
            this.Map(x => x.UserCourseId);
            this.Map(x => x.LessonId);
            this.Map(x => x.StartedDate);
            this.Map(x => x.CompletedDate);
            this.Map(x => x.IsPassed);
            this.Map(x => x.Result);
        }
    }
}
