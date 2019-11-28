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
    public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand, UpdateLessonResult>
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IElementRepository _elementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLessonCommandHandler(ILessonRepository lessonRepository, IElementRepository elementRepository, IUnitOfWork unitOfWork)
        {
            _lessonRepository = lessonRepository;
            _elementRepository = elementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateLessonResult> Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
        {
            var lesson = await _lessonRepository.GetByIdAsync(request.Id, cancellationToken);

            lesson.AllowAnswerCheck = request.AllowAnswerCheck;
            lesson.AllowAnswerPreview = request.AllowAnswerPreview;
            lesson.AttemptCount = request.AttemptCount;
            lesson.Description = request.Description;
            lesson.PassThreshold = request.PassThreshold;
            lesson.IsActive = request.IsActive;
            lesson.SetName(request.Name);
            lesson.Type = request.Type;


            var position = 0;
            var currentElements = request.Elements.Select(element => 
                new Element(request.Id, element.MaterialId)
                {
                    IsActive = element.IsActive, Position = position++, Scale = element.Scale
                })
                .ToList();

            var currentIds = currentElements.Select(x => x.Id);

            var elements = await _elementRepository.GetForLessonAsync(request.Id, cancellationToken);
            var elementIds = elements.Select(x => x.Id);

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _lessonRepository.UpdateAsync(lesson, cancellationToken);

                var elementsToAdd = currentElements.Where(x => !elementIds.Contains(x.Id));
                var elementsToDelete = elements.Where(x => !currentIds.Contains(x.Id))
                    .Select(x => x.Id);
                var elementsToUpdate = currentElements.Where(x => elementIds.Contains(x.Id));

                //await _elementRepository.DeleteAsync(elementsToDelete, cancellationToken);
                //await _elementRepository.UpdateAsync(elementsToUpdate, cancellationToken);
                //await _elementRepository.AddAsync(elementsToAdd, cancellationToken);

                await Task.WhenAll(_elementRepository.DeleteAsync(elementsToDelete, cancellationToken),
                    _elementRepository.UpdateAsync(elementsToUpdate, cancellationToken),
                    _elementRepository.AddAsync(elementsToAdd, cancellationToken));

                await transaction.CommitAsync();
            }

            return new UpdateLessonResult(lesson);
        }
    }
}
