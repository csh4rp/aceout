using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface ILessonAccessInfoRepository
    {
        Task<LessonAccessInfo> GetAsync(int userId, int lessonId, CancellationToken cancellationToken = default(CancellationToken));
        Task<LessonAccessInfo> GetForElementAsync(int userId, int elementId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
