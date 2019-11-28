using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Infrastructure.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        private readonly IPasswordPolicyRepository _passwordPolicyRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public PasswordValidator(IPasswordPolicyRepository passwordPolicyRepository, IPasswordHasher<User> passwordHasher)
        {
            _passwordPolicyRepository = passwordPolicyRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            var errors = new List<IdentityError>();
            var passwordPolicy = await _passwordPolicyRepository.GetPasswordPolicyAsync();

            var passwordHash = _passwordHasher.HashPassword(user, password);

            if(passwordPolicy == null)
            {
                return IdentityResult.Success;
            }

            if(passwordPolicy.MaxLength.HasValue && password.Length > passwordPolicy.MaxLength)
            {
                errors.Add(new IdentityError
                {
                    Code = "MaxLength",
                    Description = "Password too long"
                });
            }

            if(passwordPolicy.MinLength.HasValue && password.Length < passwordPolicy.MinLength)
            {
                errors.Add(new IdentityError
                {
                    Code = "MinLength",
                    Description = "Password too short"
                });
            }

            if(passwordPolicy.RequireSmallAndBigLetters && 
                (!password.Any(x => char.IsLower(x)) ||
                !password.Any(x => char.IsUpper(x))))
            {
                errors.Add(new IdentityError
                {
                    Code = "Case",
                    Description = "Password must contain small and big letters"
                });
            }

            if(passwordPolicy.RequireNumbers && !password.Any(x => char.IsNumber(x)))
            {
                errors.Add(new IdentityError
                {
                    Code = "Numbers",
                    Description = "Password must contain numbers"
                });
            }


            if (passwordPolicy.RequireSpecialCharacters && !password.Any(x => !char.IsLetterOrDigit(x)))
            {
                errors.Add(new IdentityError
                {
                    Code = "Special characters",
                    Description = "Password must contain special characters"
                });
            }

            if(errors.Count == 0)
            {
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(errors.ToArray());

        }
    }
}
