using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserLessons.Results;
using Aceout.Domain.Model.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserLessons.Commands
{
    public class StartLessonCommand : IRequest<UserLesson>
    {
        public int LessonId { get; }
        public int UserId { get; }

        public StartLessonCommand(int lessonId, int userId)
        {
            LessonId = lessonId > 0 ? lessonId : throw new InvalidModelException("LessonId can't be less than 1");
            UserId = userId > 0 ? userId : throw new InvalidModelException("UserId can't be less than 1");
        }
    }
}
