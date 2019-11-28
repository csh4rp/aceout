using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserLessons.Results
{
    public class UserLessonPreviewDto
    {
        public int UserLessonId { get; set; }
        public int Attempt { get; set; }
        public decimal? Result { get; set; }
        public DateTime StarteDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
