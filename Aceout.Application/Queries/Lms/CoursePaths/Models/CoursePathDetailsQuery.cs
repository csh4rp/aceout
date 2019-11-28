using Aceout.Application.Queries.Lms.CoursePaths.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.CoursePaths.Models
{
    public class CoursePathDetailsQuery : IRequest<CoursePathDto>
    {
        public int Id { get; set; }
    }
}
