using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.Materials.Commands;
using Aceout.Application.Services.Lms.TrainingMaterials.Results;
using Aceout.Domain.Factories.Lms;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Materials.Handlers
{
    public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, CreateMaterialResult>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMaterialModelConverterFactory _modelConverterFactory;
        private readonly IMaterialValidatorFactory _validatorFactory;

        public CreateMaterialCommandHandler(IMaterialRepository materialRepository,
            IMaterialModelConverterFactory modelConverterFactory,
            IMaterialValidatorFactory validatorFactory)
        {
            _materialRepository = materialRepository;
            _modelConverterFactory = modelConverterFactory;
            _validatorFactory = validatorFactory;
        }

        public async Task<CreateMaterialResult> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
        {
            var validator = _validatorFactory.CreateValidator(request.Type);
            var converter = _modelConverterFactory.Create(request.Type);
            var dataModel = converter.ConvertDataModel(request.DataModels);
            var answerModel = converter.ConvertAnswerModel(request.AnswerModels);

            var material = new Material(request.Name, request.CategoryId)
            {
                Content = request.Content,
                IsActive = request.IsActive
            };

            material.SetMaterialModel(dataModel, answerModel, request.Type);

            var validationResult = await validator.ValidateAsync(material);

            if (!validationResult.IsValid)
            {
                throw new InvalidModelException(string.Join(",\n", validationResult.Errors));
            }

            await _materialRepository.AddAsync(material, cancellationToken);

            return new CreateMaterialResult(material);
        }
    }
}
