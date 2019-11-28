using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.CoursePaths.Results
{
    public class CreateCoursePathResult
    {
        public CoursePath CoursePath { get; }

        public CreateCoursePathResult(CoursePath coursePath)
        {
            CoursePath = coursePath;
        }
    }
}
