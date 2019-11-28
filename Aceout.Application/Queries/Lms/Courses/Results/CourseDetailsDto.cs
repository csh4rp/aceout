using Aceout.Application.Model.Lms;
using System.Collections.Generic;

namespace Aceout.Application.Queries.Lms.Courses.Results
{
    public class CourseDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool RequireLessonOrder { get; set; }
        public decimal? PassThreshold { get; set; }
        public int CoursePathId { get; set; }
        public string PictureUrl { get; set; }
        public string Description { get; set; }

        public IEnumerable<CourseLessonDto> Lessons { get; set; }
        public IEnumerable<CourseGroupDetailsDto> Groups { get; set; }
    }
}
