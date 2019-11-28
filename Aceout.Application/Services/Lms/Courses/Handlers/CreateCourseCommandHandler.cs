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
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseResult>
    {
        private readonly IValidator<CreateCourseCommand> _validator;
        private readonly ICourseRepository _courseRepository;
        private readonly IGroupCourseRepository _groupCourseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(IValidator<CreateCourseCommand> validator,
            ICourseRepository courseRepository,
            IGroupCourseRepository groupCourseRepository,
            IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _courseRepository = courseRepository;
            _groupCourseRepository = groupCourseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCourseResult> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new InvalidModelException(validationResult.GetMessages());

            var course = new Course(request.CoursePathId, request.Name, request.Language)
            {
                Description = request.Description,
                PictureUrl = request.PictureUrl,
                IsActive = request.IsActive,
                PassThreshold = request.PassThreshold,
                RequireLessonOrder = request.RequireLessonOrder
            };

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                await _courseRepository.AddAsync(course, cancellationToken);

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

                await _groupCourseRepository.AddAsync(groupCourses, cancellationToken);

                await transaction.CommitAsync();
            }

            return new CreateCourseResult(course);
        }
    }
}
