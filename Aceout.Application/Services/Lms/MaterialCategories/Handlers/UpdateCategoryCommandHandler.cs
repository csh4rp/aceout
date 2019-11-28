using Aceout.Application.Services.Lms.MaterialCategories.Commands;
using Aceout.Application.Services.Lms.MaterialCategories.Results;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.MaterialCategories.Handlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        private readonly IMaterialCategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IMaterialCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.Id);
            category.SetName(request.Name);
            await _categoryRepository.UpdateAsync(category);

            return new UpdateCategoryResult(category);
        }
    }
}
