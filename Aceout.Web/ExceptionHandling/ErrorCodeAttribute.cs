using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Web.ExceptionHandling
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class ErrorCodeAttribute : Attribute
    {
        public Type ExceptionType { get; }

        public string Message { get; }

        public ErrorCodeAttribute(Type exceptionType, string message)
        {
            ExceptionType = exceptionType;
            Message = message;
        }
    }
}
