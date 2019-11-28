using Aceout.Application.Queries.Cms.Informations.Models;
using Aceout.Application.Queries.Cms.Informations.Results;
using Aceout.Domain.Model.Cms;
using Aceout.Domain.Model.Identity;
using Aceout.Domain.Model.Organization;
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
    public class InformationListQueryHandler : IRequestHandler<InformationListQuery, IEnumerable<InformationDto>>
    {
        private readonly ISession _session;

        public InformationListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<InformationDto>> Handle(InformationListQuery request, CancellationToken cancellationToken)
        {

            return await (from q in _session.Query<Information>()
                            .Where(x =>
                            (
                                (!x.FromDate.HasValue || x.FromDate.Value <= DateTime.UtcNow) &&
                                (!x.ToDate.HasValue || x.ToDate.Value >= DateTime.UtcNow)
                            ))
                          join gi in _session.Query<GroupInformation>()
                                  on q.Id equals gi.InformationId
                          from gu in gi.Group.GroupUsers
                          where gu.UserId == request.UserId
                          orderby q.CreatedDate descending
                          select new InformationDto
                          {
                              Author = gu.User.FirstName + " " + gu.User.LastName,
                              Content = q.Content,
                              Id = q.Id,
                              Title = q.Title
                          })
                          .Skip(request.Count * request.PageNumber)
                          .Take(request.Count)
                          .ToListAsync(cancellationToken);

        }
    }
}
