using Aceout.Application.Queries.Lms.UserCourses.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserCourses.Models
{
    public class UserCourseDetailsQuery : IRequest<UserCourseDetailsDto>
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }

        public bool NotCompleted { get; set; }
    }
}
