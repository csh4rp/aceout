using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.CourseReports.Models
{
    public class CourseReportDataSourceQuery : IRequest<DataSource<CourseReport>>
    {
        public int CourseId { get; set; }
        public Pager<CourseReport> Pager { get; set; }
    }
}
