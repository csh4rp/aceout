using Aceout.Domain.Model.Lms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain.Repositories.Lms
{
    public interface IUserElementInfoRepository
    {
        Task<UserElementInfo> GetAsync(int userId, int elementId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
