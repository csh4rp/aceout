using Aceout.Application.Queries.Infrastructure.Users.Models;
using Aceout.Application.Queries.Infrastructure.Users.Results;
using Aceout.Domain.Model.Identity;
using MediatR;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Infrastructure.Users.Handlers
{
    public class UserDetailsQueryHandler : IRequestHandler<UserDetailsQuery, UserDetailsDto>
    {
        private readonly ISession _session;

        public UserDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<UserDetailsDto> Handle(UserDetailsQuery request, CancellationToken cancellationToken)
        {
            var dto = (UserDetailsDto)null;
            var user = (User)null;

            var userQuery = _session.QueryOver(() => user)
                .Where(x => x.Id == request.Id)
                .SelectList(x => x.Select(() => user.Id).WithAlias(() => dto.Id)
                .Select(() => user.FirstName).WithAlias(() => dto.FirstName)
                .Select(() => user.LastName).WithAlias(() => dto.LastName)
                .Select(() => user.UserName).WithAlias(() => dto.UserName)
                .Select(() => user.Email).WithAlias(() => dto.Email)
                .Select(() => user.PhoneNumber).WithAlias(() => dto.PhoneNumber))
                .TransformUsing(Transformers.AliasToBean<UserDetailsDto>())
                .FutureValue<UserDetailsDto>();

            var userRolesQuery = _session.QueryOver<UserRole>()
                .Where(x => x.UserId == request.Id)
                .Select(x => x.RoleId)
                .Future<int>();

            dto = await userQuery.GetValueAsync();
            dto.UserRoles = await userRolesQuery.GetEnumerableAsync();

            return dto;
        }
    }
}
