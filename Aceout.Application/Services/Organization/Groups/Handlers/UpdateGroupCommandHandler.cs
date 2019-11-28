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
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, UpdateGroupResult>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGroupCommandHandler(IGroupRepository groupRepository, IGroupUserRepository groupUserRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _groupUserRepository = groupUserRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateGroupResult> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetByIdAsync(request.Id, cancellationToken);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                group.SetName(request.Name);
                await _groupRepository.UpdateAsync(group, cancellationToken);

                var users = request.UserIds.Select(x => new GroupUser(group.Id, x)).ToList();
                await _groupUserRepository.DeleteAllAsync(group.Id, cancellationToken);
                await _groupUserRepository.AddAsync(users, cancellationToken);

                await transaction.CommitAsync();
            }

            return new UpdateGroupResult(group);
        }
    }
}
