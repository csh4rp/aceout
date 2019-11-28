using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Materials.Results
{
    public class MaterialDetailsDto : MaterialDto
    {
        public int CategoryId { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public TrainingMaterialType Type { get; set; }
        public IEnumerable<DataModel> DataModels { get; set; }
        public IEnumerable<AnswerModel> AnswerModels { get; set; }
    }
}
