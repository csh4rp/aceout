using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.Courses.Results
{
    public class UpdateCourseResult
    {
        public Course Course { get; }

        public UpdateCourseResult(Course course)
        {
            Course = course;
        }
    }
}
