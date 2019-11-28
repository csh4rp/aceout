using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Common.Exceptions
{
    public class ExceptionCodeProviderAttribute : Attribute
    {
        public int StartNumber { get; }
        public Type ExceptionType { get; }

        public ExceptionCodeProviderAttribute(int startNumber, Type exceptionType)
        {
            StartNumber = startNumber;
            ExceptionType = exceptionType;
        }

    }
}
