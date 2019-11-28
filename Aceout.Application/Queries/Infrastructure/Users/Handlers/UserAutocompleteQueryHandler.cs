using Aceout.Application.Queries.Infrastructure.Users.Models;
using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Infrastructure.Users.Handlers
{
    public class UserAutocompleteQueryHandler : IRequestHandler<UserAutocompleteQuery, IEnumerable<UserDto>>
    {
        private readonly ISession _session;

        public UserAutocompleteQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<UserDto>> Handle(UserAutocompleteQuery request, CancellationToken cancellationToken)
        {

            return await _session.Query<User>()
                .Where(x =>
                    x.FirstName.StartsWith(request.SearchQuery) ||
                    x.LastName.StartsWith(request.SearchQuery))
                .Select(x => new UserDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName
                })
                .ToListAsync();

        }
    }
}
