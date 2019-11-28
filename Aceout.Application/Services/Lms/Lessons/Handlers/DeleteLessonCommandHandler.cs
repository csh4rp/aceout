using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Application.Services.Lms.Lessons.Commands;
using Aceout.Domain.Repositories.Lms;
using MediatR;

namespace Aceout.Application.Services.Lms.Lessons.Handlers
{
    public class DeleteLessonCommandHandler : AsyncRequestHandler<DeleteLessonCommand>
    {
        private readonly ILessonRepository _lessonRepository;

        public DeleteLessonCommandHandler(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        protected override Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
        {
            return _lessonRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
