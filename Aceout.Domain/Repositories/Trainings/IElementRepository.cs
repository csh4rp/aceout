using Aceout.Domain.Model.Lms;
using Aceout.Domain.Model.Trainings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IElementRepository
    {
        Task<IEnumerable<Element>> GetForLessonAsync(int lessonId, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(IEnumerable<Element> records, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync(IEnumerable<Element> elements, CancellationToken cancellationToken = default(CancellationToken));

       
    }
}
