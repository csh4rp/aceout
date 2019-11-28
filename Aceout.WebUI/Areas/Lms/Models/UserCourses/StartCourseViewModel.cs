using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.UserCourses
{
    public class StartCourseViewModel
    {
        [Required]
        public int CourseId { get; set; }
        public int UserCourseId { get; set; }
        public DateTime StartedDate { get; set; }
    }
}
