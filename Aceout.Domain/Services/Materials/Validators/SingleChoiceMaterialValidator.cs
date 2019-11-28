using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials.SingleChoice;
using Aceout.Domain.Repositories.Lms;
using Aceout.Tools.Extensions;

namespace Aceout.Domain.Services.Materials.Validators
{
    public class SingleChoiceMaterialValidator : IMaterialValidator
    {
        private readonly IMaterialRepository _materialRepository;

        public SingleChoiceMaterialValidator(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public Task<ValidationResult> ValidateAsync(Material material)
        {
            var errors = new List<string>();

            var dataModel = material.GetDataModel<SingleChoiceDataModel>();
            var answerModel = material.GetAnswerModel<SingleChoiceAnswerModel>();

            if (!dataModel.Elements.HasElements() || dataModel.Elements.Count() < 2)
            {
                errors.Add("At least two answers are required");
            }
            else if (answerModel != null && !dataModel.Elements.Any(x => x.Id == answerModel.Id))
            {
                errors.Add("Answer ID doesn't match data model answer ids");
            }

            if  (answerModel == null)
            {
                errors.Add("Answer model is required");
            }


            if (errors.Count > 0)
            {
                return Task.FromResult(ValidationResult.Invalid(errors.ToArray()));
            }

            return Task.FromResult(ValidationResult.Valid());
        }
    }
}
