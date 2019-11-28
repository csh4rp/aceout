using Aceout.Domain.Model.Lms;
using FluentNHibernate.Mapping;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Infrastructure.Database.Mappings.Lms
{
    public class LessonAccessInfoMapping : ClassMap<LessonAccessInfo>
    {
        public LessonAccessInfoMapping()
        {
            this.Table("LessonAccessInfo");
            this.ReadOnly();

            this.Id(x => x.LessonId);
            this.Map(x => x.AllowAnswerCheck);
            this.Map(x => x.AllowAnswerPreview);
            this.Map(x => x.CourseAttempt);
            this.Map(x => x.CourseCompletedDate);
            this.Map(x => x.CourseId);
            this.Map(x => x.CoursePassThreshold);
            this.Map(x => x.CourseResult);
            this.Map(x => x.CourseStartedDate);
            this.Map(x => x.GroupCourseAttemptCount);
            this.Map(x => x.GroupFromDate);
            this.Map(x => x.GroupId);
            this.Map(x => x.GroupToDate);
            this.Map(x => x.IsCourseActive);
            this.Map(x => x.IsCoursePassed);
            this.Map(x => x.IsLessonActive);
            this.Map(x => x.IsLessonPassed);
            this.Map(x => x.LessonAttemptCount);
            this.Map(x => x.LessonCompletedDate);
            this.Map(x => x.LessonPassThreshold);
            this.Map(x => x.LessonResult);
            this.Map(x => x.LessonStartedDate);
            this.Map(x => x.Position);
            this.Map(x => x.RequireLessonOrder);
            this.Map(x => x.UserCourseId);
            this.Map(x => x.UserId);
            this.Map(x => x.UserLessonAttempt);
            this.Map(x => x.UserLessonId);
            this.Map(x => x.LessonDescription);
            this.Map(x => x.LessonName);
            this.Map(x => x.LessonType).CustomType<LessonType>();
        }
    }
}
