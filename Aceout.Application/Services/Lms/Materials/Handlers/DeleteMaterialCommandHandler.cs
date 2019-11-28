using Aceout.Application.Services.Lms.Materials.Commands;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Materials.Handlers
{
    public class DeleteMaterialCommandHandler : AsyncRequestHandler<DeleteMaterialCommand>
    {
        private readonly IMaterialRepository _materialRepository;

        public DeleteMaterialCommandHandler(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        protected override Task Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
        {
            return _materialRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
