using System;
using System.Collections.Generic;
using System.Text;
using Aceout.Domain.Model.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Aceout.Web.Security.Permissions
{
    public class RequireLogOnAttribute : AuthorizeAttribute
    {
        public RequireLogOnAttribute()
        {
            this.Policy = Permission.LogOn.ToString();
            this.AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
