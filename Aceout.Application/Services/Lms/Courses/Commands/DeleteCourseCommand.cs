using Aceout.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Courses.Commands
{
    public class DeleteCourseCommand : IRequest
    {
        public int Id { get; }

        public DeleteCourseCommand(int id)
        {
            Id = id > 0 ? id : throw new InvalidModelException("Id can't be less than 1");
        }
    }
}
