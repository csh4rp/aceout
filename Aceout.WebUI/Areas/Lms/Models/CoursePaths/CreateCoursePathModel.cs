using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.CoursePaths
{
    public class CreateCoursePathModel 
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "Name field is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
    }
}
