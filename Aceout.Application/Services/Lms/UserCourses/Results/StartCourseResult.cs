using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services.Lms.UserCourses.Results
{
    public class StartCourseResult
    {
        public UserCourse UserCourse { get; }

        public StartCourseResult(UserCourse userCourse)
        {
            UserCourse = userCourse;
        }
    }
}
