using Aceout.Domain.Model.Identity;
using Aceout.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aceout.Web.Security.Permissions
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(Permission permission)
        {
            this.Policy = permission.ToString();
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }

}
