using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class CourseReportMapping : ClassMap<CourseReport>
    {
        public CourseReportMapping()
        {
            this.Table("CourseReport");
            this.Id(x => x.UserCourseId);
            this.ReadOnly();

            this.Map(x => x.UserId);
            this.Map(x => x.UserName);
            this.Map(x => x.FirstName);
            this.Map(x => x.LastName);
            this.Map(x => x.Email);
            this.Map(x => x.CourseId);
            this.Map(x => x.StartedDate);
            this.Map(x => x.CompletedDate);
            this.Map(x => x.IsPassed);
            this.Map(x => x.Result);
            this.Map(x => x.Attempt);
        }
    }
}
