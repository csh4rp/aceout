using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Model
{
    public class ErrorModel
    {
        public IEnumerable<string> Errors { get; }
        public int Code { get; }

        public ErrorModel(int code, params string[] errors)
        {
            Code = code;
            Errors = errors;
        }
    }

}
