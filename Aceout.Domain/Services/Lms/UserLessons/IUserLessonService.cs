using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Services.Lms.UserLessons
{
    public interface IUserLessonService
    {
        Task<UserLesson> StartLessonAsync(int lessonId, int userId, CancellationToken cancellationToken = default(CancellationToken));
        Task<UserLesson> FinishLessonAsync(int lessonId, int userId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
