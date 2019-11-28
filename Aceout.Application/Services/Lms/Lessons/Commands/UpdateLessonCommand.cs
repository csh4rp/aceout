using Aceout.Application.Model.Lms;
using Aceout.Application.Services.Lms.Lessons.Results;
using Aceout.Domain.Model.Lms;
using MediatR;
using System.Collections.Generic;

namespace Aceout.Application.Services.Lms.Lessons.Commands
{
    public class UpdateLessonCommand : IRequest<UpdateLessonResult>
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int? AttemptCount { get; set; }
        public bool AllowAnswerCheck { get; set; }
        public bool AllowAnswerPreview { get; set; }
        public decimal? PassThreshold { get; set; }
        public LessonType Type { get; set; }
        public IEnumerable<LessonElementModel> Elements { get; set; }
    }
}
