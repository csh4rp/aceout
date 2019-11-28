using Aceout.Application.Queries.Lms.UserCourses.Models;
using Aceout.Application.Queries.Lms.UserCourses.Results;
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

namespace Aceout.Application.Queries.Lms.UserCourses.Handlers
{
    public class UserCourseDetailsQueryHandler : IRequestHandler<UserCourseDetailsQuery, UserCourseDetailsDto>
    {
        private readonly ISession _session;

        public UserCourseDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<UserCourseDetailsDto> Handle(UserCourseDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;

            var query = _session.Query<CourseAccessInfo>()
                .Where(x => x.CourseId == request.CourseId &&
                    x.UserId == request.UserId &&
                    (
                        (!x.GroupFromDate.HasValue || x.GroupFromDate <= now) &&
                        (!x.GroupToDate.HasValue || x.GroupToDate >= now)
                    )
                );

            if (request.NotCompleted)
            {
                query = query.Where(x => !x.CompletedDate.HasValue);
            }
                                    
            var course = await query
                .Select(u => new UserCourseDetailsDto
                {
                    Attempt = u.UserCourseAttempt,
                    Description = u.CourseDescription,
                    CourseId = u.CourseId,
                    IsPassed = u.IsPassed,
                    Name = u.CourseName,
                    PictureUrl = u.PictureUrl,
                    Result = u.UserCourseResult,
                    StartedDate = u.StartedDate,
                    CompletedDate = u.CompletedDate,
                    FromDate = u.GroupFromDate,
                    ToDate = u.GroupToDate,
                    UserCourseId = u.UserCourseId,
                    AttemptLimit = u.GroupCourseAttemptCount,
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (course == null) return null;

            course.Lessons = await (from l in _session.Query<Lesson>()
                                    .Where(x => x.CourseId == request.CourseId)
                                    from ul in l.UserLessons
                                    .Where(x => x.UserCourseId == course.UserCourseId)
                                    .DefaultIfEmpty()
                                    select new UserCourseLessonDto
                                    {
                                        Description = l.Description,
                                        IsPassed = ul != null ? ul.IsPassed : (bool?)null,
                                        LessonId = l.Id,
                                        Name = l.Name,
                                        Result = ul != null ? ul.Result : (decimal?)null,
                                        Type = l.Type,
                                        IsStarted = ul != null
                                    })
                                .ToListAsync(cancellationToken);

            course.PreviousAttempts = await _session.Query<UserCourse>()
                .Where(x => x.CourseId == request.CourseId &&
                x.UserId == request.UserId &&
                x.Id != course.UserCourseId)
                .Select(c => new UserCourseDto
                {
                    Id = c.Id,
                    Attempt = c.Attempt,
                    CourseId = c.CourseId,
                    IsPassed = c.IsPassed,
                    Name = c.Course.Name,
                    PictureUrl = c.Course.PictureUrl,
                    Result = c.Result,
                    StartedDate = c.StartedDate,
                    CompletedDate = c.CompletedDate
                })
                .ToListAsync(cancellationToken);

            return course;
        }
    }
}
