using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class Course : Entity<int>
    {
        public virtual string Name { get; protected set; }
        public virtual int CoursePathId { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual bool RequireLessonOrder { get; set; }
        public virtual decimal? PassThreshold { get; set; }
        public virtual ISet<Lesson> Lessons { get; set; }
        public virtual ISet<GroupCourse> Groups { get; set; }

        protected Course() { }

        public Course(int coursePathId, string name, string language)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (coursePathId < 1)
                throw new ArgumentException(nameof(coursePathId));

            Name = name;
            CoursePathId = coursePathId;
        }


        public virtual void SetCoursePathId(int id)
        {
            CoursePathId = id > 0 ? id : throw new ArgumentException(nameof(id));
        }

        public virtual void SetName(string name)
        {
            Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentException(nameof(name));
        }
    }
}
