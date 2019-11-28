using Aceout.Application.Queries.Lms.CoursePaths.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using Aceout.Web.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.CoursePaths.Models
{
    public class CoursePathDataSourceQuery : IRequest<DataSource<CoursePathDto>>
    {
        public string SearchQuery { get; set; }
        public Pager<CoursePath> Pager { get; set; }
    }
}
