using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Services.Materials.AnswerProcessors
{
    public class ProcessedAnswer
    {
        public decimal? Result { get; set; }
        public object AnswerModel { get; set; }
    }
}
