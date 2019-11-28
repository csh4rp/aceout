using Aceout.Domain.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class UserLesson : Entity<int>
    {
        public virtual int UserId { get; protected set; }
        public virtual int LessonId { get; protected set; }
        public virtual int UserCourseId { get; set; }
        public virtual DateTime StartedDate { get; set; }
        public virtual DateTime? CompletedDate { get; set; }
        public virtual int Attempt { get; set; }
        public virtual bool IsPassed { get; set; }
        public virtual decimal? Result { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual User User { get; set; }

        protected UserLesson() { }

        public UserLesson(int userId, int lessonId, int userCourseId)
        {
            UserId = userId > 0 ? userId : throw new ArgumentException(nameof(userId));
            LessonId = lessonId > 0 ? lessonId : throw new ArgumentException(nameof(lessonId));
            UserCourseId = userCourseId > 0 ? userCourseId : throw new ArgumentException(nameof(userCourseId));
            StartedDate = DateTime.UtcNow;
        }

        public virtual void SetResult(decimal result, bool isPassed)
        {
            IsPassed = isPassed;
            Result = result;
            CompletedDate = DateTime.UtcNow;
        }
    }
}
