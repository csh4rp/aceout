using Aceout.Domain;
using NH = NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NH.ISession _session;

        public UnitOfWork(NH.ISession session)
        {
            _session = session;
        }

        public ITransaction BeginTransaction()
        {
            return new Transaction(_session.BeginTransaction());
        }

        public void Submit()
        {
            _session.Flush();
        }

        public Task SubmitAsync()
        {
            return _session.FlushAsync();
        }
    }
}
