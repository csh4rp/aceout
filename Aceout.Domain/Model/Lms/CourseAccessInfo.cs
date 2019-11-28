using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class CourseAccessInfo
    {
        public virtual int UserId { get; set; }
        public virtual int GroupId { get; set; }
        public virtual int CourseId { get; set; }
        public virtual int? UserCourseId { get; set; }
        public virtual string CourseName { get; set; }
        public virtual string CourseDescription { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual DateTime? GroupFromDate { get; set; }
        public virtual DateTime? GroupToDate { get; set; }
        public virtual bool IsCourseActive { get; set; }
        public virtual int? GroupCourseAttemptCount { get; set; }
        public virtual int? UserCourseAttempt { get; set; }
        public virtual decimal? UserCourseResult { get; set; }
        public virtual bool? IsPassed { get; set; }
        public virtual DateTime? StartedDate { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual int CoursePathId { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as CourseAccessInfo);
        }

        public virtual bool Equals(CourseAccessInfo courseAccessInfo)
        {
            if (courseAccessInfo == null) return false;

            return this.UserId == courseAccessInfo.UserId && this.CourseId == courseAccessInfo.CourseId &&
                this.UserCourseId == courseAccessInfo.UserCourseId;


        }

        public override int GetHashCode()
        {
            var hashCode = 67394686;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + CourseId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(UserCourseId);
            return hashCode;
        }
    }

    
}
