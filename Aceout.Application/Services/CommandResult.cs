using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Services
{
    public abstract class CommandResult
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }

        public CommandResult()
        {
            Errors = new string[0];
        }

        public static TResult Success<TResult>() where TResult : CommandResult, new()
        {
            var result = new TResult();
            result.IsSuccess = true;

            return result;
        } 

        public static TResult Failure<TResult>(params string[] errors) where TResult : CommandResult, new()
        {
            var result = new TResult();
            result.Errors = errors;

            return result;
        }
    }
}
