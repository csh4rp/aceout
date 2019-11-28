using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries.Infrastructure.Roles.Results
{
    public class RoleDetailsDto : RoleDto
    {
        public IEnumerable<string> Permissions { get; set; }
    }
}
