using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Organization.Models.Groups
{
    public class CreateGroupModel
    {
        /// <summary>
        /// Group name
        /// </summary>
        [Required]      
        public string Name { get; set; }

        /// <summary>
        /// IDs of users assigned to group
        /// </summary>
        public IEnumerable<int> UserIds { get; set; }
    }
}
