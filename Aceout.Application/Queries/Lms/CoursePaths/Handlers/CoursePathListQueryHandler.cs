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
    public class CoursePathListQueryHandler : IRequestHandler<CoursePathListQuery, IEnumerable<CoursePathDto>>
    {
        private readonly ISession _session;

        public CoursePathListQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<IEnumerable<CoursePathDto>> Handle(CoursePathListQuery request, CancellationToken cancellationToken)
        {
            return await _session.Query<CoursePath>()
                .Where(x => x.Language == request.Language)
                .Select(x => new CoursePathDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                })
                .ToListAsync(cancellationToken);
        }
    }
}
