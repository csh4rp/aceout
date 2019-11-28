using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Courses.Results
{
    public class CourseGroupDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? AttemptCount { get; set; }
    }
}
