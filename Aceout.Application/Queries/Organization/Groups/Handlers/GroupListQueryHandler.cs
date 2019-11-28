using Aceout.Application.Queries.Organization.Groups.Models;
using Aceout.Application.Queries.Organization.Groups.Results;
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

namespace Aceout.Application.Queries.Organization.Groups.Handlers
{
    public class GroupListQueryHandler : IRequestHandler<GroupListQuery, IEnumerable<GroupDto>>
    {
        private readonly ISession _session;

        public GroupListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<GroupDto>> Handle(GroupListQuery request, CancellationToken cancellationToken)
        {
            return await _session.Query<Group>()
                .Where(x => x.Language == request.Language)
                .Select(x => new GroupDto
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
