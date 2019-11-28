using Aceout.Application.Queries.Infrastructure.Roles.Models;
using Aceout.Application.Queries.Infrastructure.Roles.Results;
using Aceout.Domain.Model.Identity;
using MediatR;
using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Infrastructure.Roles.Handlers
{
    public class RoleDetailsQueryHandler : IRequestHandler<RoleDetailsQuery, RoleDetailsDto>
    {
        private readonly ISession _session;

        public RoleDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<RoleDetailsDto> Handle(RoleDetailsQuery request, CancellationToken cancellationToken)
        {
            var dto = (RoleDetailsDto)null;

            var futureRole = _session.QueryOver<Role>()
                .Where(x => x.Id == request.Id)
                .SelectList(l => l
                    .Select(x => x.Id).WithAlias(() => dto.Id)
                    .Select(x => x.Name).WithAlias(() => dto.Name))
                .TransformUsing(Transformers.AliasToBean<RoleDetailsDto>())
                .FutureValue<RoleDetailsDto>();

            var futurePermissions = _session.QueryOver<RolePermission>()
                        .Where(r => r.RoleId == request.Id)
                        .Select(r => r.Permission)
                        .Future<string>();

            var role = await futureRole.GetValueAsync();

            if (role == null)
            {
                return null;
            }

            role.Permissions = await futurePermissions.GetEnumerableAsync();

            return role;
        }
    }
}
