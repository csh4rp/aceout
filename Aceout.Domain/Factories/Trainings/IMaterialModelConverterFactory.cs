using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Services.Materials.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Factories.Trainings
{
    public interface IMaterialModelConverterFactory
    {
        IMaterialModelConverter Create(TrainingMaterialType type);
    }
}
