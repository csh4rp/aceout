using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Model.Materials.SingleChoice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aceout.Domain.Services.Materials.AnswerProcessors
{
    public class SingleChoiceAnswerProcessor : IAnswerProcessor
    {
        public ProcessedAnswer ProcessAnswer(Material material, IEnumerable<AnswerModel> userAnswers)
        {
            if(userAnswers.Count() != 1)
            {
                throw new ArgumentException("This type of material accepts only one answer");
            }

            var userAnswer = userAnswers.First();
            var answerModel = material.GetAnswerModel<SingleChoiceAnswerModel>();
            var userAnswerModel = new SingleChoiceAnswerModel
            {
                Id = userAnswer.Id
            };

            var processedAnswer = new ProcessedAnswer
            {
                AnswerModel = userAnswerModel
            };

            if (answerModel.Id == userAnswerModel.Id)
            {
                processedAnswer.Result = 1;
            }
            else
            {
                processedAnswer.Result = 0;
            }

            return processedAnswer;
        }
    }
}
