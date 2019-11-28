using Aceout.Application.Queries.Lms.UserLessons.Results;
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

namespace Aceout.Application.Queries.Lms.UserLessons.Handlers
{
    public class UserLessonDataSourceQueryHandler : IRequestHandler<UserLessonDataSourceQuery, DataSource<UserLessonDto>>
    {

        private readonly ISession _session;

        public UserLessonDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<UserLessonDto>> Handle(UserLessonDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<UserLesson>();

            var futureLessons = _session.Query<UserLesson>()
                .Join(_session.Query<Lesson>(),
                u => u.LessonId,
                l => l.Id,
                (u, l) => new UserLessonDto
                {
                    Description = l.Description,
                    LessonId = u.Id,
                    IsPassed = u.IsPassed,
                    Name = l.Name,
                    Result = u.Result,
                    Type = l.Type
                })
                .Paginate(request.Pager)
                .ToFuture();

            var futureCount = query.ToFutureValue(x => x.Count());

            var dataSource = new DataSource<UserLessonDto>();
            dataSource.Data = await futureLessons.GetEnumerableAsync();
            dataSource.RowCount = await futureCount.GetValueAsync();

            return dataSource;
        }
    }

}
