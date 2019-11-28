using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.CoursePaths.Results
{
    public class UpdateCoursePathResult
    {
        public CoursePath CoursePath { get; }

        public UpdateCoursePathResult(CoursePath coursePath)
        {
            CoursePath = coursePath;
        }
    }
}
