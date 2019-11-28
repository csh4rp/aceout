using Aceout.Domain.Model.Cms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Cms
{
    public interface IInformationRepository
    {
        Task AssAsync(Information entity, CancellationToken cancellationToken = default(CancellationToken));
        Task UpdateAsync(Information entity, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<Information> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
