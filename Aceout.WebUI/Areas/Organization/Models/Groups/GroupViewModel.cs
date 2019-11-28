using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Organization.Models.Groups
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> UserIds { get; set; }
    }
}
