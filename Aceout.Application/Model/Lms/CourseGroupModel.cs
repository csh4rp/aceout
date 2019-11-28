using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Model.Lms
{
    public class CourseGroupModel
    {
        public int Id { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? AttemptCount { get; set; }
    }
}
