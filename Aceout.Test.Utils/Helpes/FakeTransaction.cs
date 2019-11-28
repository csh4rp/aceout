using Aceout.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Test.Utils.Helpes
{
    public class FakeTransaction : ITransaction
    {
        public void Commit()
        {

        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {

        }
    }
}
