using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Application.Common.Exceptions;
using MediatR;

namespace Aceout.Application.Services.Lms.Lessons.Commands
{
    public class DeleteLessonCommand : IRequest
    {
        public int Id { get; }

        public DeleteLessonCommand(int id)
        {
            Id = id > 0 ? id : throw  new InvalidModelException("Id can't be less than 1");
        }
 
    }
}
