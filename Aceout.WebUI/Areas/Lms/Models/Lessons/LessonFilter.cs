using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Lessons
{
    public class LessonFilter : Paginator
    {
        public string SearchQuery { get; set; }
        public int? CourseId { get; set; }
    }
}
