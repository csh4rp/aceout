using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.CoursePaths
{
    public class UpdateCoursePathModel : CreateCoursePathModel
    {
        public int Id { get; set; }
    }
}
