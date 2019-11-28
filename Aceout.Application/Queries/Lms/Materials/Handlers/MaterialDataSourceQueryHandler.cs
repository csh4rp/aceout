using Aceout.Application.Queries.Lms.Materials.Models;
using Aceout.Application.Queries.Lms.Materials.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Infrastructure.Database;
using Aceout.Tools.Data;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Lms.Materials.Handlers
{
    public class MaterialDataSourceQueryHandler : IRequestHandler<MaterialDataSourceQuery, DataSource<MaterialDto>>
    {
        private readonly ISession _session;

        public MaterialDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public  async Task<DataSource<MaterialDto>> Handle(MaterialDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Material>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.Name.StartsWith(request.SearchQuery));
            }

            var futureCount = query.ToFutureValue(x => x.Count());

            var futureMaterial = query.Select(x => new MaterialDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .Paginate(request.Pager)
            .ToFuture<MaterialDto>();

            var dataSource = new DataSource<MaterialDto>();
            dataSource.Data = await futureMaterial.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }
}
