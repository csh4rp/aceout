using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Infrastructure.Identity;
using Aceout.Infrastructure.Repositories.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Aceout.Tests.Infrastructure.Identity
{
    public class PasswordTests
    {
        public User GetUser()
        {
            return new User();
        }

        [Fact]
        public void PasswordHash_SamePassword_HashesEqual()
        {
            var user = GetUser();
            var password = "Admin123!@#";

            var hasher = new PasswordHasher();
            var hashedPassword = hasher.HashPassword(user, password);

            var result = hasher.VerifyHashedPassword(user, hashedPassword, password);

            Assert.Equal(Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success, result);
        }

        [Fact]
        public void PasswordHash_DifferentPasswords_HashesNotEqual()
        {
            var user = GetUser();
            var password = "Admin123!@#";
            var newPassword = "12345";

            var hasher = new PasswordHasher();
            var hashedPassword = hasher.HashPassword(user, password);

            var result = hasher.VerifyHashedPassword(user, hashedPassword, newPassword);

            Assert.Equal(Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed, result);
        }

        [Fact]
        public async void PasswordPolicy_DefaultPolicy_MatchesPolicy()
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy()
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, "Admin123");

            Assert.Equal(IdentityResult.Success, result);
        }

        [Theory]
        [InlineData("Admin", 6)]
        [InlineData("Admin123", 9)]
        public async void PasswordPolicy_MinLengthPolicy_DoesntMatchPolicy(string password, int minlength)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    MinLength = minlength
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(result.Errors.Count() == 1);
            Assert.True(result.Errors.First().Code == "MinLength");

        }

        [Theory]
        [InlineData("Admin", 5)]
        [InlineData("Admin123", 8)]
        public async void PasswordPolicy_MinLengthPolicy_MatchesPolicy(string password, int minlength)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    MinLength = minlength
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.Equal(IdentityResult.Success, result);
            Assert.True(result.Errors.Count() == 0);

        }

        [Theory]
        [InlineData("Admin", 4)]
        [InlineData("Admin123", 7)]
        public async void PasswordPolicy_MaxLengthPolicy_DoesntMatchPolicy(string password, int maxLength)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    MaxLength = maxLength
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(result.Errors.Count() == 1);
            Assert.True(result.Errors.First().Code == "MaxLength");
        }

        [Theory]
        [InlineData("Admin", 6)]
        [InlineData("Admin123", 10)]
        public async void PasswordPolicy_MaxLengthPolicy_MatchesPolicy(string password, int maxLength)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    MaxLength = maxLength
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.Equal(IdentityResult.Success, result);
            Assert.True(result.Errors.Count() == 0);
        }

        [Theory]
        [InlineData("Admin")]
        [InlineData("Admin!@")]
        public async void PasswordPolicy_NumbersPolicy_DoesntMatchPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireNumbers = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(result.Errors.Count() == 1);
            Assert.True(result.Errors.First().Code == "Numbers");
        }

        [Theory]
        [InlineData("123Admin")]
        [InlineData("Admin1")]
        public async void PasswordPolicy_NumbersPolicy_MatchesPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireNumbers = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.Equal(IdentityResult.Success, result);
            Assert.True(result.Errors.Count() == 0);
        }

        [Theory]
        [InlineData("admin")]
        [InlineData("admin!@")]
        public async void PasswordPolicy_SmallBigLettersPolicy_DoesntMatchPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireSmallAndBigLetters = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(result.Errors.Count() == 1);
            Assert.True(result.Errors.First().Code == "Case");
        }

        [Theory]
        [InlineData("Admin")]
        [InlineData("Admin!@")]
        public async void PasswordPolicy_SmallBigLettersPolicy_MatchesPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireSmallAndBigLetters = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.Equal(IdentityResult.Success, result);
            Assert.True(result.Errors.Count() == 0);
        }

        [Theory]
        [InlineData("admin")]
        [InlineData("admin123")]
        public async void PasswordPolicy_SpecialCharsPolicy_DoesntMatchPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireSpecialCharacters = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(result.Errors.Count() == 1);
            Assert.True(result.Errors.First().Code == "Special characters");
        }

        [Theory]
        [InlineData("Admin!")]
        [InlineData("Admin!@")]
        public async void PasswordPolicy_SpecialCharsPolicy_MatchesPolicy(string password)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    RequireSpecialCharacters = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.Equal(IdentityResult.Success, result);
            Assert.True(result.Errors.Count() == 0);
        }

        [Theory]
        [InlineData("Haslo1", new[] { "Special characters" })]
        [InlineData("Admin123", new[] { "Special characters" })]
        [InlineData("admin", new[] { "Special characters", "MinLength", "Case", "Numbers" })]
        [InlineData("Admin123!@#6789", new[] { "MaxLength" })]
        public async void PasswordPolicy_FullPolicy_DoesntMatchPolicy(string password, string[] errors)
        {
            var user = GetUser();

            var passwordPolicyRepository = new FakePasswordPolicyRepository
            {
                Policy = new PasswordPolicy
                {
                    MaxLength = 10,
                    MinLength = 6,
                    RequireNumbers = true,
                    RequireSmallAndBigLetters = true,
                    RequireSpecialCharacters = true
                }
            };

            var passwordValidator = new PasswordValidator(passwordPolicyRepository, new PasswordHasher());

            var result = await passwordValidator.ValidateAsync(null, user, password);

            Assert.True(errors.All(result.Errors.Select(x => x.Code).ToList().Contains));
            Assert.Equal(errors.Length, result.Errors.Count());
        }

    }


    public class FakePasswordPolicyRepository : IPasswordPolicyRepository
    {
        public PasswordPolicy Policy { get; set; }

        public Task<PasswordPolicy> GetPasswordPolicyAsync()
        {
            return Task.FromResult(Policy);
        }
    }
}
