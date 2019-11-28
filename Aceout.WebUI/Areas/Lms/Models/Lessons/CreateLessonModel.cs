using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Lessons
{
    public class CreateLessonModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool AllowAnswerCheck { get; set; }
        public bool AllowAnswerPreview { get; set; }
        public int? AttemptCount { get; set; }
        public decimal? PassThreshold { get; set; }
        public LessonType Type { get; set; }
        public IEnumerable<LessonElementModel> Elements { get; set; }

    }
}
