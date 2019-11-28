using Aceout.Application.Services.Lms.UserLessons.Commands;
using Aceout.Application.Services.Lms.UserLessons.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Domain.Services.Lms.UserLessons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Application.Services.Lms.UserLessons.Handlers
{
    public class FinishLessonCommandHandler : IRequestHandler<FinishLessonCommand, UserLesson>
    {
        private readonly IUserLessonService _userLessonService;

        public FinishLessonCommandHandler(IUserLessonService userLessonService)
        {
            _userLessonService = userLessonService;
        }

        public async Task<UserLesson> Handle(FinishLessonCommand request, CancellationToken cancellationToken)
        {
            var userLesson = await _userLessonService.FinishLessonAsync(request.LessonId, request.UserId);

            return userLesson;
        }
    }
}
