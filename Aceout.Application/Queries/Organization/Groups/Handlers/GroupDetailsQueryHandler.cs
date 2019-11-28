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
    public class GroupDetailsQueryHandler : IRequestHandler<GroupDetailsQuery, GroupDetailsDto>
    {
        private readonly ISession _session;

        public GroupDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public Task<GroupDetailsDto> Handle(GroupDetailsQuery request, CancellationToken cancellationToken)
        {
            return _session.Query<Group>()
                .Where(x => x.Id == request.Id)
                .Select(x => new GroupDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Users = x.GroupUsers.Select(s => s.User)
                    .Select(s => new GroupUserDto
                    {
                        Id = s.Id,
                        FirstName = s.FirstName,
                        LastName = s.LastName,
                        UserName = s.UserName
                    }),
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
