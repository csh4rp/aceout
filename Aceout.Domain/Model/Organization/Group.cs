using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Organization
{
    public class Group : Entity<int>
    {
        public virtual string Language { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual ISet<GroupUser> GroupUsers { get; set; }
        public virtual ISet<GroupCourse> GroupCourses { get; set; }
        
        protected Group() { }

        public Group(string name, string language)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrEmpty(language))
                throw new ArgumentException(nameof(language));

            Language = language;
            Name = name;
        }

        public virtual void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }

    }
}
