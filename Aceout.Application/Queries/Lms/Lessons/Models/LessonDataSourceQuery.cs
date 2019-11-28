using Aceout.Application.Queries.Lms.Lessons.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Tools.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Lms.Lessons.Models
{
    public class LessonDataSourceQuery : IRequest<DataSource<LessonDto>>
    {
        public string SearchQuery { get; set; }
        public int? CourseId { get; set; }
        public Pager<Lesson> Pager { get; set; }
    }
}
