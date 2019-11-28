using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Domain
{
    public interface ITransaction : IDisposable
    {
        void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
