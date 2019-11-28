using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.TrainingMaterials.Results
{
    public class CreateMaterialResult
    {
        public Material Material { get; }

        public CreateMaterialResult(Material material)
        {
            Material = material;
        }
    }
}
