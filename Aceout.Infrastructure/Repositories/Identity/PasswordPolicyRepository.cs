using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aceout.Infrastructure.DataModel.Identity;
using NHibernate;
using NHibernate.Linq;

namespace Aceout.Infrastructure.Repositories.Identity
{
    public class PasswordPolicyRepository : IPasswordPolicyRepository
    {
        private readonly ISession _session;

        public PasswordPolicyRepository(ISession session)
        {
            _session = session;
        }

        public Task<PasswordPolicy> GetPasswordPolicyAsync()
        {
            return _session.Query<PasswordPolicy>()
                .FirstOrDefaultAsync();
        }
    }
}
