using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserLessons.Results
{
    public class StartLessonResult
    {
        public UserLesson UserLesson{ get; }

        public StartLessonResult(UserLesson userLesson)
        {
            UserLesson = userLesson;
        }
    }
}
