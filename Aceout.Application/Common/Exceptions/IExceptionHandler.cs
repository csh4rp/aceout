using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Common.Exceptions
{
    public interface IExceptionHandler<TException> where TException : Exception
    {
        void Handle(TException ex);
    }
}
