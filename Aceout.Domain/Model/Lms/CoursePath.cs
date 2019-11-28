using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class CoursePath : Entity<int>
    {
        public virtual string Language { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual string Description { get; set; }
        public virtual string PictureUrl { get; set; }
        public virtual ISet<Course> Courses { get; set; }
        protected CoursePath() { }

        public CoursePath(string name, string language)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrEmpty(language))
                throw new ArgumentException(nameof(language));

            Name = name;
            Language = language;
        }

        public virtual void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }
    }
}
