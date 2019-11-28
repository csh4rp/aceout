using Aceout.Domain.Model.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Aceout.Infrastructure.Identity
{
    public static class Extensions
    {
        public static int Id(this ClaimsPrincipal claimsPrincipal)
        {
            return int.TryParse(claimsPrincipal.FindFirstValue(JwtRegisteredClaimNames.Jti), out var result) ? result : 0;
        }

        public static string Username(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Name).Value;
        }

        public static IEnumerable<string> Roles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindAll(ClaimTypes.Role)
                .Select(x => x.Value);
        }

        public static IEnumerable<Permission> Permissions(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindAll(CustomClaimTypes.Permissions)
                .Select(x => (Permission)Enum.Parse(typeof(Permission), x.Value));
        }

        public static bool HasPermission(this ClaimsPrincipal claimsPrincipal, Permission permission)
        {
            return claimsPrincipal.Permissions()
                .Any(x => x == permission);
        }
    }
}
