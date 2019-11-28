using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Administration.Models.Roles
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<string> Permissions { get; set; }
    }
}
