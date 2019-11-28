using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.CoursePaths.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.CoursePaths.Commands
{
    public class UpdateCorusePathCommand : IRequest<UpdateCoursePathResult>
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public UpdateCorusePathCommand(int id, string name)
        {
            Id = id > 0 ? id : throw new InvalidModelException("Id can't be less than 1");
            Name = !string.IsNullOrEmpty(name) ? name : throw new InvalidModelException("Name can't be empty");
        }
    }
}
