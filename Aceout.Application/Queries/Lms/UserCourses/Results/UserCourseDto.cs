using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserCourses.Results
{
    public class UserCourseDto
    {
        public int? Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int? Attempt { get; set; }
        public bool? IsPassed { get; set; }
        public decimal? Result { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
