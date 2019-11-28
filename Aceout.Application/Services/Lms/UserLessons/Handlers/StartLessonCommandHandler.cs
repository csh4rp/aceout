using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserLessons.Commands;
using Aceout.Application.Services.Lms.UserLessons.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Services.Lms.UserLessons;
using Aceout.Tools.Extensions;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.UserLessons.Handlers
{
    public class StartLessonCommandHandler : IRequestHandler<StartLessonCommand, UserLesson>
    {
        private readonly IValidator<StartLessonCommand> _validator;
        private readonly IUserLessonService _userLessonService;

        public StartLessonCommandHandler(IValidator<StartLessonCommand> validator, IUserLessonService userLessonService)
        {
            _validator = validator;
            _userLessonService = userLessonService;
        }

        public async Task<UserLesson> Handle(StartLessonCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
                throw new InvalidModelException(validationResult.GetMessages());

            var userLesson = await _userLessonService.StartLessonAsync(request.LessonId, request.UserId);

            return userLesson;
        }
    }
}
