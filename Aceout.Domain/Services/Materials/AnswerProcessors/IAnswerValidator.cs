using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Services.Materials.AnswerProcessors
{
    public interface IAnswerProcessor
    {
        ProcessedAnswer ProcessAnswer(Material material, IEnumerable<AnswerModel> userAnswers);
    }
}
