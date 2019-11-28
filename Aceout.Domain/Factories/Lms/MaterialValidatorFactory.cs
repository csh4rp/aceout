using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Domain.Services.Materials.Validators;

namespace Aceout.Domain.Factories.Lms
{
    public class MaterialValidatorFactory : IMaterialValidatorFactory
    {
        private readonly IMaterialRepository _trainingMaterialRepository;

        public MaterialValidatorFactory(IMaterialRepository trainingMaterialRepository)
        {
            _trainingMaterialRepository = trainingMaterialRepository;
        }

        public IMaterialValidator CreateValidator(TrainingMaterialType trainingMaterialType)
        {
            switch (trainingMaterialType)
            {
                case TrainingMaterialType.SingleChoice:
                    return new SingleChoiceMaterialValidator(_trainingMaterialRepository);
                default:
                    break;
            }

            return null;
        }
    }
}
