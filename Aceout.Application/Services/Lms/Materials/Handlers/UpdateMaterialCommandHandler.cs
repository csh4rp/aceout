using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.Materials.Commands;
using Aceout.Application.Services.Lms.TrainingMaterials.Results;
using Aceout.Domain.Factories.Lms;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Materials.Handlers
{
    public class UpdateMaterialCommandHandler : IRequestHandler<UpdateMaterialCommand, UpdateMaterialResult>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialModelConverterFactory _modelConverterFactory;
        private readonly IMaterialValidatorFactory _validatorFactory;

        public UpdateMaterialCommandHandler(IMaterialRepository materialRepository,
            IMaterialModelConverterFactory modelConverterFactory,
            IMaterialValidatorFactory validatorFactory)
        {
            _materialRepository = materialRepository;
            _modelConverterFactory = modelConverterFactory;
            _validatorFactory = validatorFactory;
        }

        public async Task<UpdateMaterialResult> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.CreateValidator(request.Type);
            var converter = _modelConverterFactory.Create(request.Type);
            var dataModel = converter.ConvertDataModel(request.DataModels);
            var answerModel = converter.ConvertAnswerModel(request.AnswerModels);

            var material = await _materialRepository.GetByIdAsync(request.Id, cancellationToken);

            if (material == null)
                throw new EntityDoesNotExistException();

            material.SetMaterialModel(dataModel, answerModel, request.Type);
            material.Content = request.Content;
            material.SetCategory(request.CategoryId);
            material.SetName(request.Name);
            material.IsActive = request.IsActive;

            var validationResult = await validator.ValidateAsync(material);

            if (!validationResult.IsValid)
            {
                throw new InvalidModelException(string.Join(",\n", validationResult.Errors));
            }

            await _materialRepository.UpdateAsync(material, cancellationToken);

            return new UpdateMaterialResult(material);
        }
    }
}
