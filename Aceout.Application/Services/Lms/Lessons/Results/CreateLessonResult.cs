using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Lessons.Results
{
    public class CreateLessonResult
    {
        public Lesson Lesson { get; }

        public CreateLessonResult(Lesson lesson)
        {
            Lesson = lesson;
        }
    }
}
