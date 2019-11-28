using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Domain.Model.Materials;

namespace Aceout.WebUI.Areas.Lms.Models.UserElements
{
    public class UserElementModel
    {
        public int ElementId { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
    }
}
