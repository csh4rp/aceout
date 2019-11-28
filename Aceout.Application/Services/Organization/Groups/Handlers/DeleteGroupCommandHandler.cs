using Aceout.Application.Services.Organization.Groups.Commands;
using Aceout.Domain.Repositories.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Organization.Groups.Handlers
{
    public class DeleteGroupCommandHandler : AsyncRequestHandler<DeleteGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;

        public DeleteGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        protected override Task Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            return _groupRepository.DeleteAsync(request.Id);
        }
    }
}
