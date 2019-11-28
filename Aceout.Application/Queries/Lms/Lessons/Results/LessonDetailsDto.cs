using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Lessons.Results
{
    public class LessonDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public int? AttemptCount { get; set; }
        public decimal? PassThreshold { get; set; }
        public bool IsActive { get; set; }
        public bool AllowAnswerCheck { get; set; }
        public bool AllowAnswerPreview { get; set; }
        public IEnumerable<LessonElement> Elements { get; set; }
    }
}
