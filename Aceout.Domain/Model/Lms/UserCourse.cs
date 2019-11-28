using Aceout.Domain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class UserCourse : Entity<int>
    {
        public virtual int UserId { get; protected set; }
        public virtual int CourseId { get; protected set; }
        public virtual DateTime StartedDate { get; protected set; }
        public virtual int Attempt { get; protected set; }
        public virtual DateTime? CompletedDate { get; protected set; }
        public virtual bool IsPassed { get; protected set; }
        public virtual decimal? Result { get; protected set; }
        public virtual User User { get; set; }
        public virtual Course Course { get; set; }

        protected UserCourse() { }

        public UserCourse(int userId, int courseId)
        {
            if (userId <= 0)
                throw new ArgumentException(nameof(userId));

            if (courseId <= 0)
                throw new ArgumentException(nameof(courseId));

            UserId = userId;
            CourseId = courseId;
            StartedDate = DateTime.UtcNow;
        }

        public virtual void SetResult(decimal result, bool isPassed)
        {
            Result = result;
            IsPassed = isPassed;
            CompletedDate = DateTime.UtcNow;
        }
    }
}
