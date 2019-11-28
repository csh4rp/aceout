using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Services.Materials.Converters;

namespace Aceout.Domain.Factories.Trainings
{
    public class MaterialModelConverterFactory : IMaterialModelConverterFactory
    {
        public IMaterialModelConverter Create(TrainingMaterialType type)
        {
            switch (type)
            {
                case TrainingMaterialType.SingleChoice:
                    return new SingleChoiceModelConverter();
                default:
                    break;
            }

            return null;
        }
    }
}
