using Aceout.Application.Queries.Lms.Categories.Models;
using Aceout.Application.Queries.Lms.Categories.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Infrastructure.Database;
using MediatR;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Tools.Data;
using NHibernate.Linq;

namespace Aceout.Application.Queries.Lms.Categories.Handlers
{
    public class MaterialCategoryDataSourceQueryHandler : IRequestHandler<MaterialCategoryDataSourceQuery, DataSource<MaterialCategoryDto>>
    {
        private readonly ISession _session;

        public MaterialCategoryDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<MaterialCategoryDto>> Handle(MaterialCategoryDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<MaterialCategory>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.Name.StartsWith(request.SearchQuery));
            }

            var futureCount = query.ToFutureValue(x => x.Count());

            var futureCategory = query.Select(x => new MaterialCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }
            )
            .Paginate(request.Pager)
            .ToFuture();

            var dataSource = new DataSource<MaterialCategoryDto>
            {
                Data = await futureCategory.GetEnumerableAsync(cancellationToken),
                RowCount = await futureCount.GetValueAsync(cancellationToken)
            };

            return dataSource;
        }
    }
}
