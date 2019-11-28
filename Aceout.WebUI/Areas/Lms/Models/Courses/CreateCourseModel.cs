using Aceout.Application.Model.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Courses
{
    public class CreateCourseModel
    {
        public string Name { get; set; }
        public int CoursePathId { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public bool IsActive { get; set; }
        public bool RequireLessonOrder { get; set; }
        public decimal? PassThreshold { get; set; }
        public IEnumerable<CourseGroupModel> Groups { get; set; }
        public IEnumerable<int> LessonIds { get; set; }
    }
}
