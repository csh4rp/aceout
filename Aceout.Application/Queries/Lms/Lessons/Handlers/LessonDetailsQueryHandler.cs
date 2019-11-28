using Aceout.Application.Queries.Lms.Lessons.Models;
using Aceout.Application.Queries.Lms.Lessons.Results;
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

namespace Aceout.Application.Queries.Lms.Lessons.Handlers
{
    public class LessonDetailsQueryHandler : IRequestHandler<LessonDetailsQuery, LessonDetailsDto>
    {
        private readonly ISession _session;

        public LessonDetailsQueryHandler(ISession session)
        {
            _session = session;
        }

        public async Task<LessonDetailsDto> Handle(LessonDetailsQuery request, CancellationToken cancellationToken)
        {
            var futureLesson = _session.Query<Lesson>()
                .Where(x => x.Id == request.Id)
                .Select(x => new LessonDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    AllowAnswerCheck = x.AllowAnswerCheck,
                    AllowAnswerPreview = x.AllowAnswerPreview,
                    AttemptCount = x.AttemptCount,
                    Description = x.Description,
                    IsActive = x.IsActive,
                    PassThreshold = x.PassThreshold,
                })
                .ToFutureValue();

            var futureElements = _session.Query<Element>().Where(e => e.LessonId == request.Id)
                .Select(e => new LessonElement
                {
                    MaterialId = e.MaterialId,
                    MaterialName = e.Material.Name,
                    IsActive = e.IsActive,
                    Scale = e.Scale
                })
                .ToFuture();

            var lesson = await futureLesson.GetValueAsync(cancellationToken);
            lesson.Elements = await futureElements.GetEnumerableAsync(cancellationToken);

            return lesson;
        }
    }
}
