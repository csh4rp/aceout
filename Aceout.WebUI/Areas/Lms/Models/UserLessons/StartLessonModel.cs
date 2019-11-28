using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.UserLessons
{
    public class StartLessonModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int LessonId { get; set; }
    }
}
