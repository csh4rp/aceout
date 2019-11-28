using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Model.Lms
{
    public class Element : Entity<int>
    {
        public virtual int LessonId { get; protected set; }
        public virtual int MaterialId { get; protected set; }
        public virtual bool IsActive { get; set; }
        public virtual int Position { get; set; }
        public virtual int Scale { get; set; }

        public virtual Material Material { get; set; }
        public virtual Lesson Lesson { get; set; }


        protected Element() { }

        public Element(int lessonId, int materialId)
        {
            if (lessonId <= 0)
                throw new ArgumentException(nameof(lessonId));

            if (materialId <= 0)
                throw new ArgumentException(nameof(materialId));

            LessonId = lessonId;
            MaterialId = materialId;
        }
    }
}
