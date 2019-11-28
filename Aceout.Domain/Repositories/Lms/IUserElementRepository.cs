using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IUserElementRepository
    {
        Task<UserElement> GetByUserLessonId(int userLessonId, int elementId, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(UserElement entity, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(UserElement entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<decimal> GetElementsResultAsync(int userLessonId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
