using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Services.Materials.Validators;

namespace Aceout.Domain.Factories.Lms
{
    public interface IMaterialValidatorFactory
    {
        IMaterialValidator CreateValidator(TrainingMaterialType trainingMaterialType);
    }
}
