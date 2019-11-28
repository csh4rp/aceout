using Aceout.Application.Queries.Lms.CoursePaths.Models;
using Aceout.Application.Queries.Lms.CoursePaths.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Infrastructure.Database;
using MediatR;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Lms.CoursePaths.Handlers
{
    public class CoursePathDataSourceQueryHandler : IRequestHandler<CoursePathDataSourceQuery, DataSource<CoursePathDto>>
    {
        private readonly ISession _session;

        public CoursePathDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<CoursePathDto>> Handle(CoursePathDataSourceQuery request, CancellationToken cancellationToken)
        {
            var dto = (CoursePathDto)null;

            var query = _session.QueryOver<CoursePath>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(Restrictions.On<CoursePath>(x => x.Name).IsLike($"{request.SearchQuery}%"));
            }

            var futureCount = query.ToRowCountQuery()
                .FutureValue<int>();

            var futurePaths = query.SelectList(l => l
                .Select(s => s.Id).WithAlias(() => dto.Id)
                .Select(s => s.Name).WithAlias(() => dto.Name))
                .Paginate(request.Pager)
                .TransformUsing(Transformers.AliasToBean<CoursePathDto>())
                .Future<CoursePathDto>();

            var dataSource = new DataSource<CoursePathDto>();
            dataSource.Data = await futurePaths.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }
}
