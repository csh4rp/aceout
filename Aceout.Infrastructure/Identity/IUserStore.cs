using Aceout.Domain.Model.Identity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Identity
{
    public interface IUserStore: IUserStore<User>, IUserRoleStore<User>, IUserEmailStore<User>, IUserLockoutStore<User>,
        IUserPasswordStore<User>, IUserPhoneNumberStore<User>, IUserClaimStore<User>, IQueryableUserStore<User>
    {
        Task<IdentityResult> UpdateRolesAsync(User user, IEnumerable<int> roleIds, CancellationToken cancellationToken = default(CancellationToken));
        Task<IdentityResult> DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> CheckUserNameExistsAsync(string userName, int? id = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
