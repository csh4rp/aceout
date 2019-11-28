using Aceout.Domain.Model.Lms;
using Aceout.Domain.Services.Materials.AnswerProcessors;

namespace Aceout.Domain.Factories.Trainings
{
    public interface IAnswerProcessorFactory
    {
        IAnswerProcessor Create(TrainingMaterialType type);
    }
}
