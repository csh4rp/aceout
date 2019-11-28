using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserElements.Results;
using Aceout.Domain.Model.Materials;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserElements.Commands
{
    public class SaveElementCommand : IRequest<SaveElementResult>
    {
        public int UserId { get; }
        public int ElementId { get; }
        public IEnumerable<AnswerModel> Answers { get; }

        public SaveElementCommand(int userId, int elementId, IEnumerable<AnswerModel> answers)
        {
            UserId = userId > 0 ? userId : throw new InvalidModelException("UserId can't be less than 1");
            ElementId = elementId > 0 ? elementId : throw new InvalidModelException("ElementId can't be less than 1");
            Answers = answers;
        }
    }
}
