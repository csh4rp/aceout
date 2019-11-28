using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Administration.Models.Users
{
    public class UserFilter : Paginator
    {
        public string SearchQuery { get; set; }
    }
}
