using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserLessons.Results;
using Aceout.Domain.Model.Lms;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserLessons.Commands
{
    public class FinishLessonCommand : IRequest<UserLesson>
    {
        public int UserId { get; }
        public int LessonId { get; }

        public FinishLessonCommand(int userId, int lessonId)
        {
            UserId = userId > 0 ? userId : throw new InvalidModelException("UserId can't be less than 1");
            LessonId = lessonId > 0 ? lessonId : throw new InvalidModelException("LessonId can't be less than 1") ;
        }
    }
}
