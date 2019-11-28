using System;
using System.Collections.Generic;

namespace Aceout.Application.Queries.Lms.UserCourses.Results
{
    public class UserCourseDetailsDto
    {
        public int? UserCourseId { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int? Attempt { get; set; }
        public int? AttemptLimit { get; set; }
        public bool? IsPassed { get; set; }
        public decimal? Result { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IEnumerable<UserCourseLessonDto> Lessons { get; set; }
        public IEnumerable<UserCourseDto> PreviousAttempts { get; set; }
    }
}
