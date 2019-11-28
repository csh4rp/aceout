using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Common.Exceptions
{
    public class InvalidModelException : Exception
    {
        public InvalidModelException(string message) : base(message)
        {

        }
    }
}
