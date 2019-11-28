using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;

namespace Aceout.WebUI.Areas.Lms.Models.Lessons
{
    public class LessonViewModel
    {
        public int Id { get; set; }     
        public virtual int CourseId { get; protected set; }
        public virtual string Name { get; protected set; }
        public virtual LessonType Type { get; set; }
        public virtual string Description { get; set; }
        public virtual int Position { get; set; }
        public virtual int? AttemptCount { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual decimal? PassThreshold { get; set; }
        public virtual int? ElementCount { get; set; }
        public virtual bool AllowAnswerCheck { get; set; }
        public virtual bool AllowAnswerPreview { get; set; }
        public IEnumerable<LessonElementModel> Elements { get; set; }
    }
}
