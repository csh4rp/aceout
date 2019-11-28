using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Organization;
using System;
using System.Collections.Generic;
using System.Text;


namespace Aceout.Domain.Model.Lms
{
    public class GroupCourse
    {
        public virtual int GroupId { get; set; }
        public virtual int CourseId { get; set; }
        public virtual DateTime? FromDate { get; set; }
        public virtual DateTime? ToDate { get; set; }
        public virtual int? AttemptCount { get; set; }
        public virtual Group Group { get; set; }
        public virtual Course Course { get; set; }

        protected GroupCourse() { }

        public GroupCourse(int groupId, int courseId)
        {
            if (groupId < 1)
                throw new ArgumentException(nameof(groupId));

            if (courseId < 1)
                throw new ArgumentException(nameof(courseId));

            GroupId = groupId;
            CourseId = courseId;
        }

        public virtual void SetDates(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate.Value > toDate.Value)
                throw new ArgumentException(nameof(fromDate) + " can't be later than " + nameof(toDate));

            FromDate = fromDate;
            ToDate = toDate;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GroupCourse);
        }

        public virtual bool Equals(GroupCourse obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GroupId == this.GroupId && obj.CourseId == this.CourseId)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1211552702;
            hashCode = hashCode * -1521134295 + GroupId.GetHashCode();
            hashCode = hashCode * -1521134295 + CourseId.GetHashCode();
            return hashCode;
        }
    }
}
