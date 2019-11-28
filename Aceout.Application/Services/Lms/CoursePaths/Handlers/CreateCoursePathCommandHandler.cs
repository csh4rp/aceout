using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Application.Services.Lms.CoursePaths.Commands;
using Aceout.Application.Services.Lms.CoursePaths.Results;
using Aceout.Application.Services.Lms.Courses.Commands;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using MediatR;

namespace Aceout.Application.Services.Lms.CoursePaths.Handlers
{
    public class CreateCoursePathCommandHandler : IRequestHandler<CreateCoursePathCommand, CreateCoursePathResult>
    {
        private readonly ICoursePathRepository _coursePathRepository;

        public CreateCoursePathCommandHandler(ICoursePathRepository coursePathRepository)
        {
            _coursePathRepository = coursePathRepository;
        }

        public async Task<CreateCoursePathResult> Handle(CreateCoursePathCommand request, CancellationToken cancellationToken)
        {
            var coursePath = new CoursePath(request.Name, request.Language);

            await _coursePathRepository.AddAsync(coursePath);

            return new CreateCoursePathResult(coursePath);
        }
    }
}
