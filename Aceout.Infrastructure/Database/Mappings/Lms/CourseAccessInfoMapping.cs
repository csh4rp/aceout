using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class CourseAccessInfoMapping : ClassMap<CourseAccessInfo>
    {
        public CourseAccessInfoMapping()
        {
            this.Table("CourseAccessInfo");
            this.ReadOnly();

            this.Id(x => x.UserId);
            this.Map(x => x.CourseId);
            this.Map(x => x.UserCourseId);
            this.Map(x => x.CompletedDate);
            this.Map(x => x.CourseName);
            this.Map(x => x.CourseDescription);
            this.Map(x => x.GroupCourseAttemptCount);
            this.Map(x => x.GroupFromDate);
            this.Map(x => x.GroupId);
            this.Map(x => x.GroupToDate);
            this.Map(x => x.IsCourseActive).CustomSqlType("BIT");
            this.Map(x => x.IsPassed).CustomSqlType("BIT");
            this.Map(x => x.PictureUrl);
            this.Map(x => x.StartedDate);
            this.Map(x => x.UserCourseAttempt);
            this.Map(x => x.UserCourseResult);
            this.Map(x => x.CoursePathId);

        }
    }
}
