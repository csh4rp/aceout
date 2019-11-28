using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aceout.Domain.Model.Identity;

namespace Aceout.Web.Security.Permissions
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (requirement.Permission == Permission.LogOn && context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }
            else if (context.User.HasPermission(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
