using Aceout.Application.Services.Lms.Lessons.Commands;
using Aceout.Application.Services.Lms.Lessons.Results;
using Aceout.Domain;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Lessons.Handlers
{
    public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, CreateLessonResult>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IElementRepository _elementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLessonCommandHandler(ILessonRepository lessonRepository, IElementRepository elementRepository, IUnitOfWork unitOfWork)
        {
            _lessonRepository = lessonRepository;
            _elementRepository = elementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateLessonResult> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = new Lesson(request.CourseId, request.Name, request.Type)
            {
                AllowAnswerCheck = request.AllowAnswerCheck,
                AllowAnswerPreview = request.AllowAnswerPreview,
                AttemptCount = request.AttemptCount,
                Description = request.Description,
                PassThreshold = request.PassThreshold,
                IsActive = request.IsActive,
            };

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _lessonRepository.AddAsync(lesson);

                var index = 0;
                var elements = request.Elements.Select(element =>
                        new Element(lesson.Id, element.MaterialId)
                        {
                            IsActive = element.IsActive,
                            Position = index++,
                            Scale = element.Scale
                        })
                    .ToList();

                await _elementRepository.AddAsync(elements, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }

            return new CreateLessonResult(lesson);
        }
    }
}
