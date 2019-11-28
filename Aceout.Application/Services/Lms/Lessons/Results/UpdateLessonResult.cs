using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Lessons.Results
{
    public class UpdateLessonResult : CommandResult
    {
        public Lesson Lesson { get; }

        public UpdateLessonResult(Lesson lesson)
        {
            Lesson = lesson;
        }
    }
}
