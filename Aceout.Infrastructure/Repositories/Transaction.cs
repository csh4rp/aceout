using Aceout.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories
{
    public class Transaction : Domain.ITransaction
    {
        private readonly NHibernate.ITransaction _transaction;

        public Transaction(NHibernate.ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return _transaction.CommitAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
