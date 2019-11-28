using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.CoursePaths
{
    public class CoursePathFilter : Paginator
    {
        public string SearchQuery { get; set; }
    }
}
