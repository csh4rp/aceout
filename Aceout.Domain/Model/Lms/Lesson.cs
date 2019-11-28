using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;

namespace Aceout.Domain.Model.Lms
{
    public class Lesson : Entity<int>
    { 
        public virtual int CourseId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual LessonType Type { get; set; }
        public virtual string Description { get; set; }
        public virtual int Position { get; set; }
        public virtual int? AttemptCount { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual decimal? PassThreshold { get; set; }
        public virtual bool AllowAnswerCheck { get; set; }
        public virtual bool AllowAnswerPreview { get; set; }

        public virtual ISet<Element> Elements { get; protected set; }
        public virtual ISet<UserLesson> UserLessons { get; protected set; }

        protected Lesson() { }

        public Lesson(int courseId, string name, LessonType type)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            CourseId = courseId;
            Name = name;
            Type = type;
        }

        public virtual void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }

    } 
}
