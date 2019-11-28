using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserLessons.Results
{
    public class UserLessonDetailsDto
    {
        public int? UserLessonId { get; set; }
        public int LessonId { get; set; }
        public LessonType Type { get; set; }
        public string Name { get; set; }
        public bool? IsPassed { get; set; }
        public decimal? Result { get; set; }
        public string Description { get; set; }
        public int? Attempt { get; set; }
        public int? AttemptLimit { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int ElementCount { get; set; }
        public bool IsAccessible { get; set; }
        public bool AllowAnswerCheck { get; set; }
        public IEnumerable<UserLessonPreviewDto> PreviousAttempts { get; set; }
    }
}
