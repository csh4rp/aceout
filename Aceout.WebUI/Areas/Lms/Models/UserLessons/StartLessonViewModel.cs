using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.UserLessons
{
    public class StartLessonViewModel
    {
        public int LessonId { get; set; }
        public int UserLessonId { get; set; }
        public DateTime StartedDate { get; set; }
    }
}
