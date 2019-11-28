using Aceout.Infrastructure.DataModel.Identity;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Repositories.Identity
{
    public interface IPasswordPolicyRepository
    {
        Task<PasswordPolicy> GetPasswordPolicyAsync();
    }
}
