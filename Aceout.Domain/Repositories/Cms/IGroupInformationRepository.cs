using Aceout.Domain.Model.Cms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Cms
{
    public interface IGroupInformationRepository
    {
        Task AddAsync(IEnumerable<GroupInformation> entities, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteAsync(int informationId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
