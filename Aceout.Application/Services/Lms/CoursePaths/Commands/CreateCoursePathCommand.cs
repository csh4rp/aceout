using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.CoursePaths.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.CoursePaths.Commands
{
    public class CreateCoursePathCommand : IRequest<CreateCoursePathResult>
    {
        public string Language { get; }
        public string Name { get;  }
        public string Description { get; }
        public string PictureUrl { get; set; }

        public CreateCoursePathCommand(string language, string name, string description)
        {
            Language = !string.IsNullOrEmpty(language) ? language : throw new InvalidModelException("Language can't be empty");
            Name = !string.IsNullOrEmpty(name) ? name : throw new InvalidModelException("Name can't be empty"); ;
            Description = description;
        }
    }
}
