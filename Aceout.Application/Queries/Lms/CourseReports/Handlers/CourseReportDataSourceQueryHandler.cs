using Aceout.Application.Queries.Lms.CourseReports.Models;
using Aceout.Domain.Model.Lms;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Tools.Data;

namespace Aceout.Application.Queries.Lms.CourseReports.Handlers
{
    public class CourseReportDataSourceQueryHandler : IRequestHandler<CourseReportDataSourceQuery, DataSource<CourseReport>>
    {
        private readonly ISession _session;

        public CourseReportDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<CourseReport>> Handle(CourseReportDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<CourseReport>()
                .Where(x => x.CourseId == request.CourseId);

            var reportFuture = query.Paginate(request.Pager)
                .ToFuture();

            var countFuture = query.ToFutureValue(x => x.Count());

            var dataSource = new DataSource<CourseReport>
            {
                Data = await reportFuture.GetEnumerableAsync(cancellationToken),
                RowCount = await countFuture.GetValueAsync(cancellationToken)
            };

            return dataSource;
        }
    }
}
