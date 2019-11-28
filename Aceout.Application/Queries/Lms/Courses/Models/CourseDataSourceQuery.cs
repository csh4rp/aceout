using Aceout.Application.Queries.Lms.Courses.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Courses.Models
{
    public class CourseDataSourceQuery : IRequest<DataSource<CourseDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<Course> Pager { get; set; }
    }
}
