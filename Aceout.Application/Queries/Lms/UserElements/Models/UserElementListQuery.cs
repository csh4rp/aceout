using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Application.Common.Exceptions;
using Aceout.Application.Queries.Lms.UserElements.Results;
using MediatR;

namespace Aceout.Application.Queries.Lms.UserElements.Models
{
    public class UserElementListQuery : IRequest<IEnumerable<UserElementDetailsDto>>
    {
        public int UserId { get;  }
        public int LessonId { get; }

        public UserElementListQuery(int userId, int lessonId)
        {
            UserId = userId > 0 ? userId : throw  new InvalidModelException("UserId can't be less than 1");
            LessonId = lessonId > 0 ? lessonId : throw new InvalidModelException("LessonId can't be less than 1");
        }
    }
}
