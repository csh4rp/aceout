using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Materials;

namespace Aceout.Application.Queries.Lms.Elements.Results
{
    public class ElementAnswerData
    {
        public int ElementId { get; set; }
        public  IEnumerable<AnswerModel> Answers { get; set; }
    }
}
