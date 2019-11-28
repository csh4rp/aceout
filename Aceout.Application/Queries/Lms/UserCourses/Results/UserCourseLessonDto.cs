using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserCourses.Results
{
    public class UserCourseLessonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LessonType Type { get; set; }
        public int? AttemptCount { get; set; }
        public decimal? PassResult { get; set; }
        public bool IsActive { get; set; }
        public bool? IsPassed { get; set; }
        public int LessonId { get; set; }
        public decimal? Result { get; set; }
        public bool IsStarted { get; set; }
    }
}
