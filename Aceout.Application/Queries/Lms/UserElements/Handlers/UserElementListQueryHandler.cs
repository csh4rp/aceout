using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.UserElements.Models;
using Aceout.Application.Queries.Lms.UserElements.Results;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Tools.Extensions;
using MediatR;
using NHibernate;
using NHibernate.Linq;

namespace Aceout.Application.Queries.Lms.UserElements.Handlers
{
    public class UserElementListQueryHandler : IRequestHandler<UserElementListQuery, IEnumerable<UserElementDetailsDto>>
    {
        private readonly ISession _session;
        private readonly IMaterialModelConverterFactory _converterFactory;

        public UserElementListQueryHandler(ISession session, IMaterialModelConverterFactory converterFactory)
        {
            _session = session;
            _converterFactory = converterFactory;
        }

        public async Task<IEnumerable<UserElementDetailsDto>> Handle(UserElementListQuery request, CancellationToken cancellationToken)
        {
            var elements = await _session.Query<UserElementInfo>()
                .Where(x => x.UserId == request.UserId &&
                            x.LessonId == request.LessonId &&
                            x.IsCourseActive &&
                            x.IsLessonActive &&
                            x.IsElementActive &&
                            x.IsMaterialActive
                )
                .Select(x => new
                {
                    x.ElementId,
                    x.MaterialType,
                    x.MaterialName,
                    x.MaterialContent,
                    x.MaterialDataModel,
                    x.UserAnswerModel,
                    x.ElementPosition,
                    x.LessonId,
                    x.MaterialAnswerModel,
                    x.AllowAnswerCheck
                })
                .ToListAsync(cancellationToken);

            var materialTypes = elements.Select(x => x.MaterialType)
                .Distinct();

            var converters = materialTypes.ToDictionary(k => k, v => _converterFactory.Create(v));

            return elements.ConvertAll(e =>
                new UserElementDetailsDto
                {
                    LessonId = e.LessonId,
                    MaterialType = e.MaterialType,
                    MaterialName = e.MaterialName,
                    MaterialContent = e.MaterialContent,
                    ElementId = e.ElementId,
                    Position = e.ElementPosition,
                    MaterialDataModels = converters[e.MaterialType].ConvertDataModel(e.MaterialDataModel),
                    UserAnswerModels = converters[e.MaterialType].ConvertUserAnswerModel(e.UserAnswerModel),
                    CorrectAnswerModels = e.AllowAnswerCheck ? 
                        converters[e.MaterialType].ConvertAnswerModel(e.MaterialAnswerModel) : Enumerable.Empty<AnswerModel>()
                }
            );
        }
    }
}
