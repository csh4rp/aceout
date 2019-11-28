using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Web.Security.Permissions
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission Permission { get; }

        public PermissionRequirement(Permission permission)
        {
            Permission = permission;
        }
    }
}
