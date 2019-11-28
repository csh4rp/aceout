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
    public class CoursePathDetailsQueryHandler : IRequestHandler<CoursePathDetailsQuery, CoursePathDto>
    {
        private readonly ISession _session;

        public CoursePathDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public Task<CoursePathDto> Handle(CoursePathDetailsQuery request, CancellationToken cancellationToken)
        {
            return _session.Query<CoursePath>()
                .Where(x => x.Id == request.Id)
                .Select(x => new CoursePathDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    PictureUrl = x.PictureUrl
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
