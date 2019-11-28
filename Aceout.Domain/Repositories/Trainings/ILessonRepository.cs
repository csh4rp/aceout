using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System.Threading.Tasks;
using  System.Threading;

namespace Aceout.Domain.Repositories.Lms
{
    public interface ILessonRepository
    {
        Task AddAsync(Lesson lesson, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(Lesson lesson, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<Lesson> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
