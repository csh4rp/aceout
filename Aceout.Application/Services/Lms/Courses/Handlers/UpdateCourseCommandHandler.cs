using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.Courses.Commands;
using Aceout.Application.Services.Lms.Courses.Results;
using Aceout.Domain;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Repositories.Lms;
using Aceout.Tools.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.Courses.Handlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdateCourseResult>
    {
        private readonly IValidator<UpdateCourseCommand> _validator;
        private readonly ICourseRepository _courseRepository;
        private readonly IGroupCourseRepository _groupCourseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCourseCommandHandler(IValidator<UpdateCourseCommand> validator,
            ICourseRepository courseRepository,
            IGroupCourseRepository groupCourseRepository,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _courseRepository = courseRepository;
            _groupCourseRepository = groupCourseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCourseResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new InvalidModelException(validationResult.GetMessages());

            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);

            if (course == null)
                throw new EntityDoesNotExistException();

            course.SetCoursePathId(request.CoursePathId);
            course.SetName(request.Name);
            course.PictureUrl = request.PictureUrl;
            course.IsActive = request.IsActive;
            course.RequireLessonOrder = request.RequireLessonOrder;
            course.PassThreshold = request.PassThreshold;

            var groupCourses = request.Groups.Select(x =>
            {
                var model = new GroupCourse(x.Id, course.Id)
                {
                    AttemptCount = x.AttemptCount
                };

                model.SetDates(x.FromDate, x.ToDate);
                return model;
            })
            .ToList();

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await Task.WhenAll(_courseRepository.UpdateAsync(course, cancellationToken),
                    _groupCourseRepository.DeleteForCourseAsync(course.Id, cancellationToken),
                    _groupCourseRepository.AddAsync(groupCourses, cancellationToken));

                await transaction.CommitAsync(cancellationToken);
            }

            return new UpdateCourseResult(course);
        }
    }
}
