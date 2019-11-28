using Aceout.Application.Model.Lms;
using Aceout.Application.Services.Lms.Courses.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Courses.Commands
{
    public class CreateCourseCommand : IRequest<CreateCourseResult>
    {
        public int CoursePathId { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool RequireLessonOrder { get; set; }
        public decimal? PassThreshold { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public IEnumerable<CourseGroupModel> Groups { get; set; }

    }
}
