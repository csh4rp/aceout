using Aceout.Application.Services.Lms.CoursePaths.Commands;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.CoursePaths.Handlers
{
    public class DeleteCoursePathCommandHandler : AsyncRequestHandler<DeleteCoursePathCommand>
    {
        private readonly ICoursePathRepository _coursePathRepository;

        public DeleteCoursePathCommandHandler(ICoursePathRepository coursePathRepository)
        {
            _coursePathRepository = coursePathRepository;
        }

        protected override Task Handle(DeleteCoursePathCommand request, CancellationToken cancellationToken)
        {
            return _coursePathRepository.DeleteAsync(request.Id);
        }
    }
}
