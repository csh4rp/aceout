using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Lessons.Results
{
    public class LessonElement
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public int MaterialId { get; set; }
        public bool IsActive { get; set; }
        public int Scale { get; set; }
    }
}
