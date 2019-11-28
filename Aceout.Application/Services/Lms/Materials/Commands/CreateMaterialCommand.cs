using Aceout.Application.Services.Lms.TrainingMaterials.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using MediatR;
using System.Collections.Generic;

namespace Aceout.Application.Services.Lms.Materials.Commands
{
    public class CreateMaterialCommand : IRequest<CreateMaterialResult>
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
        public TrainingMaterialType Type { get; set; }
        public IEnumerable<DataModel> DataModels { get; set; }
        public IEnumerable<AnswerModel> AnswerModels { get; set; }
    }
}
