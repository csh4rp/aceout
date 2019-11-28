using Aceout.Application.Queries.Lms.UserCourses.Models;
using Aceout.Application.Queries.Lms.UserCourses.Results;
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
using Aceout.Domain.Model.Organization;

namespace Aceout.Application.Queries.Lms.UserCourses.Handlers
{
    public class UserCourseDataSourceQueryHandler : IRequestHandler<UserCourseDataSourceQuery, DataSource<UserCourseDto>>
    {
        private readonly ISession _session;

        public UserCourseDataSourceQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<DataSource<UserCourseDto>> Handle(UserCourseDataSourceQuery request, CancellationToken cancellationToken)
        {
            var query = _session.Query<CourseAccessInfo>()
                .Where(x => x.UserId == request.UserId &&
                            x.IsCourseActive);

            if (request.CoursePathId.HasValue)
            {
                query = query.Where(x => x.CoursePathId == request.CoursePathId);
            }

            var futureCourses = query.Select(x => new UserCourseDto
            {
                CourseId = x.CourseId,
                Name = x.CourseName,
                PictureUrl = x.PictureUrl,
                Id = x.UserCourseId != null ? x.UserCourseId : (int?)null,
                Attempt = x.UserCourseAttempt != null ? x.UserCourseAttempt : (int?)null,
                IsPassed = x.IsPassed != null ? x.IsPassed : (bool?)null,
                Result = x.UserCourseResult != null ? x.UserCourseResult : (decimal?)null,
                StartedDate = x.StartedDate,
                CompletedDate = x.CompletedDate
            })
            .Paginate(request.Pager).ToFuture();

            var futureCount = query.ToFutureValue(x => x.Count());

            var dataSource = new DataSource<UserCourseDto>
            {
                Data = await futureCourses.GetEnumerableAsync(cancellationToken),
                RowCount = await futureCount.GetValueAsync(cancellationToken)
            };

            return dataSource;
        }
    }
}
