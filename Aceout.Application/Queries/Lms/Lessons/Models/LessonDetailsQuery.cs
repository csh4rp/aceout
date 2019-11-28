using Aceout.Application.Queries.Lms.Lessons.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Lessons.Models
{
    public class LessonDetailsQuery : IRequest<LessonDetailsDto>
    {
        public int Id { get; set; }
    }
}
