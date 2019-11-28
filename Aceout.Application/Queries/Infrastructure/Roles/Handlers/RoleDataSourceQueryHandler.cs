using Aceout.Application.Queries.Infrastructure.Roles.Models;
using Aceout.Application.Queries.Infrastructure.Roles.Results;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Database;
using MediatR;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Infrastructure.Roles.Handlers
{
    public class RoleDataSourceQueryHandler : IRequestHandler<RoleDataSourceQuery, DataSource<RoleDto>>
    {
        private readonly ISession _session;

        public RoleDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<RoleDto>> Handle(RoleDataSourceQuery request, CancellationToken cancellationToken)
        {
            var dto = (RoleDto)null;
            var query = _session.QueryOver<Role>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(Restrictions.On<Role>(x => x.Name).IsLike(request.SearchQuery + "%"));
            }

            var futureQuery = query.SelectList(l => l
                .Select(x => x.Name).WithAlias(() => dto.Name)
                .Select(x => x.Id).WithAlias(() => dto.Id))
                .Paginate(request.Pager)
                .TransformUsing(Transformers.AliasToBean<RoleDto>())
                .Future<RoleDto>();

            var futureCount = query.ToRowCountQuery()
                .FutureValue<int>();

            var dataSource = new DataSource<RoleDto>();
            dataSource.Data = await futureQuery.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }
}
