using Aceout.Application.Services.Organization.Groups.Commands;
using Aceout.Application.Services.Organization.Groups.Results;
using Aceout.Domain;
using Aceout.Domain.Model.Organization;
using Aceout.Domain.Repositories.Organization;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Organization.Groups.Handlers
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, CreateGroupResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupUserRepository _groupUserRepository;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IGroupUserRepository groupUserRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
        }

        public async Task<CreateGroupResult> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new Group(request.Name, request.Language);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _groupRepository.AddAsync(group);

                var users = request.UserIds.Select(x => new GroupUser(group.Id, x))
                    .ToList();

                await _groupUserRepository.AddAsync(users);

                await _unitOfWork.SubmitAsync();
                await transaction.CommitAsync();
            }

            return new CreateGroupResult(group);
        }
    }
}
