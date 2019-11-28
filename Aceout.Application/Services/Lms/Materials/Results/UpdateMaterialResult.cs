using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.TrainingMaterials.Results
{
    public class UpdateMaterialResult
    {
        public Material Material { get; }

        public UpdateMaterialResult(Material material)
        {
            Material = material;
        }
    }
}
