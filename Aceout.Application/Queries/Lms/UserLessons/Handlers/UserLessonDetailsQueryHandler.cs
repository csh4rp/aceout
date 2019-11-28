using Aceout.Application.Queries.Lms.UserLessons.Models;
using Aceout.Application.Queries.Lms.UserLessons.Results;
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

namespace Aceout.Application.Queries.Lms.UserLessons.Handlers
{
    public class UserLessonDetailsQueryHandler : IRequestHandler<UserLessonDetailsQuery, UserLessonDetailsDto>
    {
        private readonly ISession _session;

        public UserLessonDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<UserLessonDetailsDto> Handle(UserLessonDetailsQuery request, CancellationToken cancellationToken)
        {
            var lesson = await _session.Query<LessonAccessInfo>()
                .Where(x => x.LessonId == request.LessonId &&
                            x.UserId == request.UserId)
            .Select(x => new UserLessonDetailsDto
            {
                UserLessonId = x.UserLessonId,
                LessonId = x.LessonId,
                Attempt = x.UserLessonAttempt,
                CompletedDate = x.LessonCompletedDate,
                StartedDate = x.LessonStartedDate,
                Description = x.LessonDescription,
                Name = x.LessonName,
                Type = x.LessonType,
                IsPassed = x.IsLessonPassed,
                Result = x.LessonResult,
                AttemptLimit = x.LessonAttemptCount,
                AllowAnswerCheck = x.AllowAnswerCheck,
                IsAccessible = ((!x.GroupFromDate.HasValue || x.GroupFromDate.Value <= DateTime.UtcNow) &&
                                (!x.GroupToDate.HasValue || x.GroupToDate.Value >= DateTime.UtcNow)),
                ElementCount = _session.Query<Element>()
                    .Where(e => e.LessonId == x.LessonId &&
                           e.Material.IsActive)
                    .Count()
            })
            .FirstOrDefaultAsync(cancellationToken);

            if (lesson.UserLessonId.HasValue)
            {
                lesson.PreviousAttempts = _session.Query<UserLesson>()
                    .Where(x => x.Id != lesson.UserLessonId.Value &&
                           x.LessonId == request.LessonId &&
                           x.UserId == request.UserId)
                    .Select(x => new UserLessonPreviewDto
                    {
                        Attempt = x.Attempt,
                        Result = x.Result,
                        UserLessonId = x.Id,
                        CompletedDate = x.CompletedDate,
                        StarteDate = x.StartedDate
                    })
                    .ToList();
            }

            return lesson;
        }
    }
}
