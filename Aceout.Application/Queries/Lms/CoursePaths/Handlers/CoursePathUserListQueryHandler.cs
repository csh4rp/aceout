using Aceout.Application.Queries.Lms.CoursePaths.Models;
using Aceout.Application.Queries.Lms.CoursePaths.Results;
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

namespace Aceout.Application.Queries.Lms.CoursePaths.Handlers
{
    public class CoursePathUserListQueryHandler : IRequestHandler<CoursePathUserListQuery, IEnumerable<CoursePathDto>>
    {
        private readonly ISession _session;

        public CoursePathUserListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<CoursePathDto>> Handle(CoursePathUserListQuery request, CancellationToken cancellationToken)
        {
            var result = await _session.Query<CourseAccessInfo>()
                .Where(x => x.UserId == request.UserId)
                .Join(_session.Query<CoursePath>(),
                c => c.CoursePathId,
                p => p.Id,
                (c, p) => new CoursePathDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    PictureUrl = p.PictureUrl
                })
                .Distinct()
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
