using Aceout.Application.Services.Lms.Courses.Commands;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Courses.Handlers
{
    public class DeleteCourseCommandHandler : AsyncRequestHandler<DeleteCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        protected override Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return _courseRepository.DeleteAsync(request.Id);
        }
    }
}
