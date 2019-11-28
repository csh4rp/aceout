using Aceout.Application.Queries.Lms.Lessons.Models;
using Aceout.Application.Queries.Lms.Lessons.Results;
using Aceout.Domain.Model.Lms;
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

namespace Aceout.Application.Queries.Lms.Lessons.Handlers
{
    public class LessonDataSourceQueryHandler : IRequestHandler<LessonDataSourceQuery, DataSource<LessonDto>>
    {
        private readonly ISession _session;

        public LessonDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<LessonDto>> Handle(LessonDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<Lesson>();

            if (!string.IsNullOrEmpty(request.SearchQuery))
            {
                query = query.Where(x => x.Name.StartsWith(request.SearchQuery));
            }

            if (request.CourseId.HasValue)
            {
                query = query.Where(x => x.CourseId == request.CourseId.Value);
            }

            var futureCount = query.ToFutureValue(x => x.Count());

            var furureLessons = query.Select(x => new LessonDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToFuture();

            var dataSource = new DataSource<LessonDto>
            {
                Data = await furureLessons.GetEnumerableAsync(cancellationToken),
                RowCount = await futureCount.GetValueAsync(cancellationToken)
            };

            return dataSource;
        }
    }
}
