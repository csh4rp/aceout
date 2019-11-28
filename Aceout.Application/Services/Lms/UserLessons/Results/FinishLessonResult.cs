using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserLessons.Results
{
    public class FinishLessonResult 
    {
        public decimal Result { get; }
        public bool IsPassed { get; }

        public FinishLessonResult(decimal result, bool isPassed)
        {
            Result = result;
            IsPassed = isPassed;
        }
    }
}
