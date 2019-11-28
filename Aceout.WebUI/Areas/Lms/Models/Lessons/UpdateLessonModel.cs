using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Lessons
{
    public class UpdateLessonModel : CreateLessonModel
    {
        [Required]
        public int Id { get; set; }
    }
}
