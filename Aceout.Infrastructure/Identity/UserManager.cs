using Aceout.Domain;
using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Tools.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public class UserManager : UserManager<User>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger,
            IUnitOfWork unitOfWork) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this._unitOfWork = unitOfWork;
        }

        public override Task<IdentityResult> UpdateAsync(User user)
        {
            return Store.UpdateAsync(user, CancellationToken);
        }


        public Task<IdentityResult> UpdateRolesAsync(User user, IEnumerable<int> roleIds)
        {
            var store = this.Store as IUserStore;

            return store.UpdateRolesAsync(user, roleIds, this.CancellationToken);
        }

        public Task<IdentityResult> DeleteAsync(int id)
        {
            var store = this.Store as IUserStore;

            return store.DeleteAsync(id, this.CancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password, IEnumerable<int> roleIds)
        {
            var store = this.Store as IUserStore;

            using (var transaction = _unitOfWork.BeginTransaction())
            {
                var userResult = await this.CreateAsync(user, password);
                var roleResult = await store.UpdateRolesAsync(user, roleIds);

                await transaction.CommitAsync();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user, string password, IEnumerable<int> roleIds)
        {
            var store = this.Store as IUserStore;

            using (var transaction = _unitOfWork.BeginTransaction())
            {

                if (!string.IsNullOrEmpty(password))
                {
                    foreach (var validator in this.PasswordValidators)
                    {
                        var validationResult = await validator.ValidateAsync(this, user, password);
                        if (!validationResult.Succeeded)
                        {
                            return validationResult;
                        }

                    }

                    user.PasswordHash = this.PasswordHasher.HashPassword(user, password);
                }


                foreach (var validator in this.UserValidators)
                {
                    var validationResult = await validator.ValidateAsync(this, user);
                    if (!validationResult.Succeeded)
                    {
                        return validationResult;
                    }
                }

               
                var userResult = await this.UpdateAsync(user);
                var roleResult = await store.UpdateRolesAsync(user, roleIds);

                await transaction.CommitAsync();
            }

            return IdentityResult.Success;
        }

        public Task<bool> CheckUserNameExistsAsync(string userName, int? id = null)
        {
            return (this.Store as IUserStore).CheckUserNameExistsAsync(userName, id, this.CancellationToken);
        }

    }
}
