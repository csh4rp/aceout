using Aceout.Application.Services.Lms.MaterialCategories.Commands;
using Aceout.Application.Services.Lms.MaterialCategories.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.MaterialCategories.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        private readonly IMaterialCategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(IMaterialCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new MaterialCategory(request.Name, request.Language);
            await _categoryRepository.AddAsync(category);

            return new CreateCategoryResult(category);
        }
    }
}
