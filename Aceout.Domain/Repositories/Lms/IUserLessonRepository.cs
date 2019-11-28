using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IUserLessonRepository
    {
        Task AddAsync(UserLesson record, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(UserLesson record, CancellationToken cancellationToken = default(CancellationToken));
        Task<UserLesson> GetByIdAsync(int userLessonId, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> ArePreviousPassedAsync(int lessonId, int userCourseId, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> AreAllLessonsPassedAsync(int userCourseId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
