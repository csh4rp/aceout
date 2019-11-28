using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class LessonAccessInfo
    {
        public virtual int UserId { get; set; }
        public virtual int GroupId { get; set; }
        public virtual int CourseId { get; set; }
        public virtual int LessonId { get; set; }
        public virtual int? UserLessonId { get; set; }
        public virtual int? UserCourseId { get; set; }
        public virtual DateTime? GroupFromDate { get; set; }
        public virtual DateTime? GroupToDate { get; set; }
        public virtual bool RequireLessonOrder { get; set; }
        public virtual decimal? CoursePassThreshold { get; set; }
        public virtual bool IsCourseActive { get; set; }
        public virtual bool IsLessonActive { get; set; }
        public virtual int? LessonAttemptCount { get; set; }
        public virtual int? GroupCourseAttemptCount { get; set; }
        public virtual int Position { get; set; }
        public virtual bool AllowAnswerCheck { get; set; }
        public virtual bool AllowAnswerPreview { get; set; }
        public virtual DateTime? CourseStartedDate { get; set; }
        public virtual DateTime? CourseCompletedDate { get; set; }
        public virtual int? CourseAttempt { get; set; }
        public virtual bool? IsCoursePassed { get; set; }
        public virtual decimal? CourseResult { get; set; }
        public virtual int? UserLessonAttempt { get; set; }
        public virtual DateTime? LessonStartedDate { get; set; }
        public virtual DateTime? LessonCompletedDate { get; set; }
        public virtual bool? IsLessonPassed { get; set; }
        public virtual decimal? LessonResult { get; set; }
        public virtual decimal? LessonPassThreshold { get; set; }
        public virtual string LessonName { get; set; }
        public virtual string LessonDescription { get; set; }
        public virtual LessonType LessonType { get; set; }
    }
}
