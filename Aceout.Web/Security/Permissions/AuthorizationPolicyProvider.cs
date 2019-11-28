using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Web.Security.Permissions
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private static readonly object locker = new object();
        private readonly AuthorizationOptions _options;

        public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            
            var policy = await base.GetPolicyAsync(policyName);

            if (policy != null) return policy;


            lock (locker)
            {
                var permission = (Permission)Enum.Parse(typeof(Permission), policyName);

                policy = new AuthorizationPolicyBuilder()
                    .AddRequirements(new PermissionRequirement(permission))
                    .Build();

                _options.AddPolicy(policyName, policy);
            }

            return policy;

        }
    }
}
