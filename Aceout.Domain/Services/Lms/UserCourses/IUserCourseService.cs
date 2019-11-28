using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Lms.UserCourses
{
    public interface IUserCourseService
    {
        Task<UserCourse> StartCourseAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
