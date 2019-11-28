using Aceout.Application.Queries.Lms.UserLessons.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.UserLessons.Models
{
    public class UserLessonDetailsQuery : IRequest<UserLessonDetailsDto>
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }
    }
}
