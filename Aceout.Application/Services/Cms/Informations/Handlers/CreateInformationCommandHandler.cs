using Aceout.Application.Services.Cms.Informations.Commands;
using Aceout.Domain;
using Aceout.Domain.Model.Cms;
using Aceout.Domain.Repositories.Cms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Cms.Informations.Handlers
{
    public class CreateInformationCommandHandler : IRequestHandler<CreateInformationCommand, Information>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInformationRepository _informationRepository;
        private readonly IGroupInformationRepository _groupInformationRepository;

        public CreateInformationCommandHandler(IUnitOfWork unitOfWork, 
            IInformationRepository informationRepository, 
            IGroupInformationRepository groupInformationRepository)
        {
            _unitOfWork = unitOfWork;
            _informationRepository = informationRepository;
            _groupInformationRepository = groupInformationRepository;
        }

        public async Task<Information> Handle(CreateInformationCommand request, CancellationToken cancellationToken)
        {
            var information = new Information(request.UserId, request.Title, request.Content)
            {
                FromDate = request.FromDate,
                ToDate = request.ToDate,
            };

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _informationRepository.AssAsync(information, cancellationToken);

                var groups = request.GroupIds.Select(x => new GroupInformation(x, information.Id))
                    .ToList();

                await _groupInformationRepository.AddAsync(groups, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }

            return information;
        }
    }
}
