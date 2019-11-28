using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.UserCourses
{
    public class UserCoursesFilter : Paginator
    {
        public int? CoursePathId { get; set; }
    }
}
