using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Materials;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IMaterialRepository
    {
        Task<Material> GetByElementIdAsync(int elementId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Material> GetForUserAsync(int userLessonId, int number, CancellationToken cancellationToken = default(CancellationToken));
        Task<Material> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(Material material, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(Material material, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
