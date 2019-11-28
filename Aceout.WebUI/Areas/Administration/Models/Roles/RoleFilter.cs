using Aceout.Infrastructure.DataModel.Identity;
using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Administration.Models.Roles
{
    public class RoleFilter : Paginator
    {
        public string SearchQuery { get; set; }
    }
}
