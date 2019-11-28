using Aceout.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Test.Utils.Helpes
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public ITransaction BeginTransaction()
        {
            return new FakeTransaction();
        }

        public void Submit()
        {

        }

        public Task SubmitAsync()
        {
            return Task.CompletedTask;
        }
    }
}
