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
    public class GroupAutocompleteQueryHandler : IRequestHandler<GroupAutocompleteQuery, IEnumerable<GroupDto>>
    {
        private readonly ISession _session;

        public GroupAutocompleteQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<GroupDto>> Handle(GroupAutocompleteQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Group>()
                .Where(x => x.Language == request.Language);

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.Name.StartsWith(request.SearchQuery));
            }

            return await query.Select(x => new GroupDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(cancellationToken);
        }
    }
}
