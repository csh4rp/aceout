using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Courses
{
    public class CourseDetailsViewModel : CreateCourseModel
    {
        public IEnumerable<int> LessonIds { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
    }
}
