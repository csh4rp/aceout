using Aceout.Application.Queries.Lms.Courses.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Courses.Models
{
    public class CourseDetailsQuery : IRequest<CourseDetailsDto>
    {
        public int Id { get; set; }
    }
}
