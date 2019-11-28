using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class LessonReport
    {
        public virtual int UserLessonId { get; set; }
        public virtual int UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual int CourseId { get; set; }
        public virtual int UserCourseId { get; set; }
        public virtual int LessonId { get; set; }
        public virtual DateTime StartedDate { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual bool IsPassed { get; set; }
        public virtual decimal? Result { get; set; }
        public virtual int Attempt { get; set; }
    }
}
