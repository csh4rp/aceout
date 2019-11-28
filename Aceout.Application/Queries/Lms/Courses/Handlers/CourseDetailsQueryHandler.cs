using Aceout.Application.Queries.Lms.Courses.Models;
using Aceout.Application.Queries.Lms.Courses.Results;
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

namespace Aceout.Application.Queries.Lms.Courses.Handlers
{
    public class CourseDetailsQueryHandler : IRequestHandler<CourseDetailsQuery, CourseDetailsDto>
    {
        private readonly ISession _session;

        public CourseDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<CourseDetailsDto> Handle(CourseDetailsQuery request, CancellationToken cancellationToken)
        {
            var courseFuture = _session.Query<Course>()
                .Where(x => x.Id == request.Id)
                .Select(x => new CourseDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CoursePathId = x.CoursePathId,
                    PictureUrl = x.PictureUrl,
                    IsActive = x.IsActive,
                    PassThreshold = x.PassThreshold,
                    RequireLessonOrder = x.RequireLessonOrder
                })
                .ToFutureValue();

            var groupsFuture = _session.Query<GroupCourse>()
                .Where(x => x.CourseId == request.Id)
                .Select(g => new CourseGroupDetailsDto
                {
                    Id = g.Group.Id,
                    Name = g.Group.Name,
                    AttemptCount = g.AttemptCount,
                    FromDate = g.FromDate,
                    ToDate = g.ToDate
                })
                .ToFuture();

            var lessonsFuture = _session.Query<Lesson>()
                .Where(x => x.CourseId == request.Id)
                .Select(l => new CourseLessonDto
                {
                    Id = l.Id,
                    Name = l.Name
                })
                .ToFuture();

            var result = await courseFuture.GetValueAsync(cancellationToken);
            result.Groups = await groupsFuture.GetEnumerableAsync(cancellationToken);
            result.Lessons = await lessonsFuture.GetEnumerableAsync(cancellationToken);

            return result;
        }
    }
}
