using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Domain.Services
{
    public class ValidationResult
    {
        #region Props
        public bool IsValid { get; }
        public string[] Errors { get; private set; }
        #endregion

        #region Ctor
        private ValidationResult(bool isValid)
        {
            Errors = new string[0];
            IsValid = isValid;
        }

        private ValidationResult(string[] errors)
        {
            IsValid = false;
            Errors = errors;
        }
        #endregion

        #region Methods
        public static ValidationResult Valid()
        {
            return new ValidationResult(true);
        }

        public static ValidationResult Invalid(params string[] errors)
        {
            return new ValidationResult(errors);
        }
        #endregion

    }
}
