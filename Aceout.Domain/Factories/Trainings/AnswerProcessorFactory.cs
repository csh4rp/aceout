using System;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Services.Materials.AnswerProcessors;

namespace Aceout.Domain.Factories.Trainings
{
    public class AnswerProcessorFactory : IAnswerProcessorFactory
    {
        public IAnswerProcessor Create(TrainingMaterialType type)
        {
            switch (type)
            {
                case TrainingMaterialType.SingleChoice:
                    return new SingleChoiceAnswerProcessor();
                default:
                    throw new ArgumentException("Validator for specified type doesn't exist");
            }
        }
    }
}
