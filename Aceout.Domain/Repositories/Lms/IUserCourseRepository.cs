using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IUserCourseRepository
    {
        Task<UserCourse> GetLastAttemptAsync(int userId, int courseId, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(UserCourse userCourse, CancellationToken cancellationToken = default(CancellationToken));
        Task<UserCourse> GetByIdAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken));
        Task<decimal> GetLessonsResultAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(UserCourse userCourse, CancellationToken cancellationToken = default(CancellationToken));
    }
}
