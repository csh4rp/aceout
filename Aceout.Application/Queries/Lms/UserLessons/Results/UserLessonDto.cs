using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserLessons.Results
{
    public class UserLessonDto
    {
        public int LessonId { get; set; }
        public LessonType Type { get; set; }
        public string Name { get; set; }
        public bool? IsPassed { get; set; }
        public decimal? Result { get; set; }
        public string Description { get; set; }
    }
}
