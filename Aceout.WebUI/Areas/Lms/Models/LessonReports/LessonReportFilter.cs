using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.LessonReports
{
    public class LessonReportFilter : Paginator
    {
        public int? LessonId { get; set; }
    }
}
