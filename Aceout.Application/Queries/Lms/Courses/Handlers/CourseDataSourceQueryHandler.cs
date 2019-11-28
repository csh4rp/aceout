using Aceout.Application.Queries.Lms.Courses.Models;
using Aceout.Application.Queries.Lms.Courses.Results;
using Aceout.Domain.Model.Lms;
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

namespace Aceout.Application.Queries.Lms.Courses.Handlers
{
    public class CourseDataSourceQueryHandler : IRequestHandler<CourseDataSourceQuery, DataSource<CourseDto>>
    {
        private readonly ISession _session;

        public CourseDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<CourseDto>> Handle(CourseDataSourceQuery request, CancellationToken cancellationToken)
        {
            var dto = (CourseDto)null;

            var query = _session.QueryOver<Course>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(Restrictions.On<Course>(x => x.Name).IsLike($"{request.SearchQuery}%"));
            }

            var futureCount = query.ToRowCountQuery()
                .FutureValue<int>();

            var futureCourses = query.SelectList(l => l
                .Select(s => s.Id).WithAlias(() => dto.Id)
                .Select(s => s.Name).WithAlias(() => dto.Name))
                .Paginate(request.Pager)
                .TransformUsing(Transformers.AliasToBean<CourseDto>())
                .Future<CourseDto>();

            var dataSource = new DataSource<CourseDto>();
            dataSource.Data = await futureCourses.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }
}
