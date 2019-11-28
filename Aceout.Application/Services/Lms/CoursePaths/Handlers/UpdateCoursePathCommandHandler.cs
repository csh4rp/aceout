using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.CoursePaths.Commands;
using Aceout.Application.Services.Lms.CoursePaths.Results;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.CoursePaths.Handlers
{
    public class UpdateCoursePathCommandHandler : IRequestHandler<UpdateCorusePathCommand, UpdateCoursePathResult>
    {
        private readonly ICoursePathRepository _coursePathRepository;

        public UpdateCoursePathCommandHandler(ICoursePathRepository coursePathRepository)
        {
            _coursePathRepository = coursePathRepository;
        }

        public async Task<UpdateCoursePathResult> Handle(UpdateCorusePathCommand request, CancellationToken cancellationToken)
        {
            var coursePath = await _coursePathRepository.GetByIdAsync(request.Id);

            if (coursePath == null)
                throw new EntityDoesNotExistException();

            coursePath.SetName(request.Name);
            coursePath.Description = request.Description;

            await _coursePathRepository.UpdateAsync(coursePath);

            return new UpdateCoursePathResult(coursePath);
        }
    }
}
