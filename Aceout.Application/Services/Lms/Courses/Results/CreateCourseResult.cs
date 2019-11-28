using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Courses.Results
{
    public class CreateCourseResult
    {
        public Course Course { get; }

        public CreateCourseResult(Course course)
        {
            Course = course;
        }
    }
}
