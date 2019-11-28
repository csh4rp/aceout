using Aceout.Application.Services.Lms.MaterialCategories.Commands;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.MaterialCategories.Handlers
{
    public class DeleteCategoryCommandHandler : AsyncRequestHandler<DeleteCategoryCommand>
    {
        private readonly IMaterialCategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(IMaterialCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        protected override Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            return _categoryRepository.DeleteAsync(request.Id);
        }
    }
}
