using Aceout.Application.Services.Cms.Informations.Commands;
using Aceout.Domain.Repositories.Cms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Cms.Informations.Handlers
{
    public class DeleteInformationCommandHandler : AsyncRequestHandler<DeleteInformationCommand>
    {
        private readonly IInformationRepository _informationRepository;

        public DeleteInformationCommandHandler(IInformationRepository informationRepository)
        {
            _informationRepository = informationRepository;
        }

        protected override Task Handle(DeleteInformationCommand request, CancellationToken cancellationToken)
        {
            return _informationRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
