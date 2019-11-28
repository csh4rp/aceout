using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Tools.Extensions
{
    public static class ThirdPartyExtensions
    {
        public static string GetMessages(this ValidationResult validationResult)
        {
            if (validationResult.IsValid) return "";

            var messages = validationResult.Errors.Select(x => x.ErrorMessage).ToArray();

            return string.Join(", " + Environment.NewLine, messages);
        }
    }
}
