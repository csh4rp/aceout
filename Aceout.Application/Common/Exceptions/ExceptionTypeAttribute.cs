using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Common.Exceptions
{
    public class ExceptionTypeAttribute : Attribute
    {
        public Type ExceptionType { get; }
        public string Message { get; set; }

        public ExceptionTypeAttribute(Type exceptionType, string message)
        {
            ExceptionType = exceptionType;
            Message = message;
        }
    }
}
