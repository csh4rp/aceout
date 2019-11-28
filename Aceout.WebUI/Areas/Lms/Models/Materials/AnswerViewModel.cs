using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Materials
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuestionId { get; set; }
    }
}
