using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Application.Queries.Lms.Elements.Results;
using Aceout.Domain.Model.Materials;
using MediatR;

namespace Aceout.Application.Queries.Lms.Elements.Models
{
    public class ElementsAnswersQuery : IRequest<IEnumerable<ElementAnswerData>>
    {
        public int LessonId { get; }

        public ElementsAnswersQuery(int lessonId)
        {
            LessonId = lessonId;
        }

    }
}
