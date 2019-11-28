using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Domain
{
    public interface IUnitOfWork
    {
        ITransaction BeginTransaction();
        void Submit();
        Task SubmitAsync();
    }
}
