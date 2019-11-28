using Aceout.Application.Common.Exceptions;
using Aceout.Application.Services.Lms.UserCourses.Results;
using Aceout.Application.Services.Lms.UserLessons.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserCourses.Commands
{
    public class StartCourseCommand : IRequest<StartCourseResult>
    {
        public int CourseId { get; }
        public int UserId { get; }

        public StartCourseCommand(int courseId, int userId)
        {
            CourseId = courseId > 0 ? courseId : throw new InvalidModelException("CourseId can't be less than 1");
            UserId = userId > 0 ? userId : throw new InvalidModelException("UserId can't be less than 1");
        }



        
    }
}
