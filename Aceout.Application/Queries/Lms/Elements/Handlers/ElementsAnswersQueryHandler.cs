using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Application.Queries.Lms.Elements.Models;
using Aceout.Application.Queries.Lms.Elements.Results;
using Aceout.Domain.Factories.Trainings;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using Aceout.Domain.Services.Materials.Converters;
using MediatR;
using NHibernate;
using NHibernate.Linq;

namespace Aceout.Application.Queries.Lms.Elements.Handlers
{
    public class ElementsAnswersQueryHandler : IRequestHandler<ElementsAnswersQuery, IEnumerable<ElementAnswerData>>
    {
        private readonly ISession _session;
        private readonly IMaterialModelConverterFactory _modelConverterFactory;

        public ElementsAnswersQueryHandler(ISession session, IMaterialModelConverterFactory modelConverterFactory)
        {
            _session = session;
            _modelConverterFactory = modelConverterFactory;
        }

        public async Task<IEnumerable<ElementAnswerData>> Handle(ElementsAnswersQuery request, CancellationToken cancellationToken)
        {
            var elements = await _session.Query<Element>()
                .Where(x => x.LessonId == request.LessonId &&
                            x.IsActive)
                .Join(_session.Query<Material>().Where(m => m.IsActive),
                    e => e.MaterialId,
                    m => m.Id,
                    (e, m) => new
                    {
                        e.Id,
                        m.Type,
                        m.AnswerModel
                    })
                .ToListAsync(cancellationToken);

            var types = elements.Select(x => x.Type)
                .Distinct();

            var converters = types.ToDictionary(k => k, v => _modelConverterFactory.Create(v));

            return elements.ConvertAll(e => new ElementAnswerData
            {
                ElementId = e.Id,
                Answers = converters[e.Type].ConvertAnswerModel(e.AnswerModel)
            });
        }
    }
}
