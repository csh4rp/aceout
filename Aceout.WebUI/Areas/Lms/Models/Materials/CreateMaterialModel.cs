using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.Materials
{
    public class CreateMaterialModel
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public TrainingMaterialType Type { get; set; }
        public List<AnswerModel> AnswerModels { get; set; }
        public List<DataModel> DataModels { get; set; }

    }
}
