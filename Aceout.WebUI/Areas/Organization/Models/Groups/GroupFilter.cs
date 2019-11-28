using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Organization.Models.Groups
{
    public class GroupFilter : Paginator
    {
        public string SearchQuery { get; set; }
    }
}
