using Aceout.Application.Queries.Organization.Groups.Models;
using Aceout.Application.Queries.Organization.Groups.Results;
using Aceout.Domain.Model.Organization;
using Aceout.Infrastructure.Database;
using MediatR;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Organization.Groups.Handlers
{
    public class GroupDataSourceQueryHandler : IRequestHandler<GroupDataSourceQuery, DataSource<GroupDto>>
    {
        private readonly ISession _session;

        public GroupDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<GroupDto>> Handle(GroupDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Group>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.Name.StartsWith(request.SearchQuery));
            }

            var futureCount = query.ToFutureValue(x => x.Count());
                

            var futureCategory = query.Select(x => new GroupDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToFuture();

            var dataSource = new DataSource<GroupDto>();
            dataSource.Data = await futureCategory.GetEnumerableAsync(cancellationToken);
            dataSource.RowCount = await futureCount.GetValueAsync(cancellationToken);

            return dataSource;
        }
    }
}
