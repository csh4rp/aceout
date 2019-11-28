using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.LessonReports.Model
{
    public class LessonReportDataSourceQuery : IRequest<DataSource<LessonReport>>
    {
        public int? LessonId { get; set; }
        public Pager<LessonReport> Pager { get; set; }
    }
}
