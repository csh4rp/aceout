using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.CourseReports
{
    public class CourseReportFilter : Paginator
    {
        public int CourseId { get; set; }
    }
}
