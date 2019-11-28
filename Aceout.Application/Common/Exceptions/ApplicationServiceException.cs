using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Common.Exceptions
{
    [Serializable]
    public class ApplicationServiceException : Exception
    {
        public int ErrorCode { get; set; }

        public ApplicationServiceException(int errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
