using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Lessons
{
    public class LessonElementModel
    {
        public int MaterialId { get; set; }
        public bool IsActive { get; set; }
        public int Scale { get; set; }
    }
}
