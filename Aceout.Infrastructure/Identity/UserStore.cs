using Aceout.Tools.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aceout.Infrastructure.DataModel.Identity;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using Aceout.Tools.Helpers;
using System.Security.Claims;
using Aceout.Infrastructure.Database;
using System.Linq;
using NHibernate.Linq;
using Aceout.Domain.Model.Identity;

namespace Aceout.Infrastructure.Identity
{
    public class UserStore : IUserStore
    {
        private readonly ISession _session;

        public IQueryable<User> Users => _session.Query<User>();

        public UserStore(ISession session)
        {
            _session = session;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            user.ModifiedDate = user.CreatedDate = DateTime.UtcNow;

            await _session.SaveAsync(user);
            _session.Flush();
            _session.Clear();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(user);
                await transaction.CommitAsync(cancellationToken);
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _session.GetAsync<User>(int.Parse(userId), cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _session.Query<User>()
                .Where(x => x.UserName == normalizedUserName)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return GetNormalizedUserNameAsync(user, cancellationToken);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.UserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            return SetNormalizedUserNameAsync(user, userName, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _session.Update(user);
            await _session.FlushAsync();

            return IdentityResult.Success;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = await _session.Query<Role>()
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var userRole = new UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            };

            await _session.SaveAsync(userRole, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = await _session.Query<Role>()
                .Where(x => x.Name == roleName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var userRole = await _session.LoadAsync<UserRole>(new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            },
                cancellationToken);

            await _session.DeleteAsync(userRole, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return await _session.Query<UserRole>()
                .Where(x => x.UserId == user.Id)
                .Join(_session.Query<Role>(),
                    u => u.RoleId,
                    r => r.Id,
                    (u, r) => r.Name)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return _session.Query<Role>()
                .Where(x => x.Name == roleName &&
                    _session.Query<UserRole>()
                    .Where(s => s.RoleId == x.Id &&
                        s.UserId == user.Id).Any())
                .AnyAsync(cancellationToken);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await (from r in _session.Query<Role>()
                          where r.Name == roleName
                          join ur in _session.Query<UserRole>()
                          on r.Id equals ur.RoleId
                          join u in _session.Query<User>()
                          on ur.UserId equals u.Id
                          select u)
                   .ToListAsync();
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.IsEmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.IsEmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _session.Query<User>()
                .Where(x => x.Email == normalizedEmail)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.Email = normalizedEmail;
            return Task.CompletedTask;
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
        {
            var date = user.LockoutEndDate.HasValue ? new DateTimeOffset(user.LockoutEndDate.Value) : (DateTimeOffset?)null;
            return Task.FromResult(date);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEndDate = lockoutEnd.HasValue ? lockoutEnd.Value.DateTime : (DateTime?)null;
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount++;
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = 0;
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
        {
            var lockoutEnabled = user.LockoutEndDate.HasValue && user.LockoutEndDate > DateTime.UtcNow;
            return Task.FromResult(lockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
        {
            user.IsLockoutEnabled = enabled;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash.IsEmpty());
        }

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.IsPhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            user.IsPhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancellationToken)
        {
            var dbClaims = await _session.Query<UserClaim>()
                .Where(x => x.UserId == user.Id)
                .Select(x => new
                {
                    x.Type,
                    x.Value
                })
                .ToListAsync();

            return dbClaims.Select(x => new Claim(x.Type, x.Value))
                .ToList();
        }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            var userClaims = claims.Select(x => new UserClaim(user.Id, x.Type, x.Value));

            using (var transaction = _session.BeginTransaction())
            {
                foreach (var claim in userClaims)
                {
                    _session.Save(claim);
                }

                await transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            using (var transaction = _session.BeginTransaction())
            {
                var dbClaim = _session.Query<UserClaim>()
                    .Where(x => x.UserId == user.Id &&
                        x.Type == claim.Type &&
                        x.Value == claim.Value)
                        .FirstOrDefault();

                dbClaim.SetType(newClaim.Type);
                dbClaim.SetValue(newClaim.Value);

                _session.Update(dbClaim);

                await transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (var claim in claims)
                {
                    var dbClaim = _session.QueryOver<UserClaim>()
                        .Where(x => x.UserId == user.Id &&
                            x.Type == claim.Type &&
                            x.Value == claim.Value)
                            .SingleOrDefault();

                    _session.Delete(dbClaim);
                }

                await transaction.CommitAsync(cancellationToken);
            }
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return await _session.Query<UserClaim>()
                .Where(x => x.Type == claim.Type &&
                    x.Value == claim.Value)
                    .Join(_session.Query<User>(),
                    c => c.UserId,
                    u => u.Id,
                    (c, u) => u)
                    .ToListAsync(cancellationToken);

        }


        public async Task<IdentityResult> UpdateRolesAsync(User user, IEnumerable<int> roleIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            var deleteQuery = _session.Query<UserRole>()
            .Where(x => x.UserId == user.Id)
            .Delete();

            await _session.FlushAsync(cancellationToken);

            foreach (var roleId in roleIds)
            {
                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = roleId
                };

                _session.Save(userRole);
            }

            await _session.FlushAsync(cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            _session.Query<User>()
                .Where(x => x.Id == id)
                .Delete();
            await _session.FlushAsync();
            _session.Clear();

            return IdentityResult.Success;
        }

        public Task<bool> CheckUserNameExistsAsync(string userName, int? id = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (id.HasValue)
            {
                return _session.Query<User>()
                    .AnyAsync(x =>
                    x.UserName == userName &&
                    x.Id != id.Value,
                    cancellationToken);
            }

            return _session.Query<User>()
                .AnyAsync(x =>
                x.UserName == userName,
                cancellationToken);
        }
    }

}
