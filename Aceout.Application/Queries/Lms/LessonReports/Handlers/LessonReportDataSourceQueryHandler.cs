using Aceout.Application.Queries.Lms.LessonReports.Model;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Queries.Lms.LessonReports.Handlers
{
    public class LessonReportDataSourceQueryHandler : IRequestHandler<LessonReportDataSourceQuery, DataSource<LessonReport>>
    {
        private readonly ISession _session;

        public LessonReportDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<LessonReport>> Handle(LessonReportDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<LessonReport>();

            if (request.LessonId.HasValue)
            {
                query = query.Where(x => x.LessonId == request.LessonId.Value);
            }

            var reportFuture = query.Paginate(request.Pager)
                .ToFuture();

            var countFuture = query.ToFutureValue(x => x.Count());

            var dataSource = new DataSource<LessonReport>
            {
                Data = await reportFuture.GetEnumerableAsync(cancellationToken),
                RowCount = await countFuture.GetValueAsync(cancellationToken)
            };

            return dataSource;
        }
    }
}
