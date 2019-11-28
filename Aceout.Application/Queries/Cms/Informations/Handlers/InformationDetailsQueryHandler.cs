using Aceout.Application.Queries.Cms.Informations.Models;
using Aceout.Application.Queries.Cms.Informations.Results;
using Aceout.Domain.Model.Cms;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Cms.Informations.Handlers
{
    public class InformationDetailsQueryHandler : IRequestHandler<InformationDetailsQuery, InformationDetailsDto>
    {
        private readonly ISession _session;

        public InformationDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public Task<InformationDetailsDto> Handle(InformationDetailsQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Information>()
                .Where(x => x.Id == request.Id)
                .Select(x => new InformationDetailsDto
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    FromDate = x.FromDate,
                    ToDate = x.ToDate,
                    UserId = x.UserId,
                    Groups = x.GroupInformations.Select(s => new GroupInformationDto
                    {
                        Id = s.GroupId,
                        Name = s.Group.Name
                    })
                });


            return query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
