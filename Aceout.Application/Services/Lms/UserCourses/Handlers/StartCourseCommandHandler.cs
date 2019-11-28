using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserCourses.Commands;
using Aceout.Application.Services.Lms.UserCourses.Results;
using Aceout.Domain.Services.Lms.UserCourses;
using Aceout.Tools.Extensions;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.UserCourses.Handlers
{
    public class StartCourseCommandHandler : IRequestHandler<StartCourseCommand, StartCourseResult>
    {
        private readonly IValidator<StartCourseCommand> _validator;
        private readonly IUserCourseService _userCourseService;

        public StartCourseCommandHandler(IValidator<StartCourseCommand> validator, IUserCourseService userCourseService)
        {
            _validator = validator;
            _userCourseService = userCourseService;
        }

        public async Task<StartCourseResult> Handle(StartCourseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                throw new InvalidModelException(validationResult.GetMessages());

            var userCourse = await _userCourseService.StartCourseAsync(request.UserId, request.CourseId);

            return new StartCourseResult(userCourse);

        }
    }
}
