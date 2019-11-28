using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Lms.Models.MaterialCategory
{
    public class MaterialCategoryFilter : Paginator
    {
        public string SearchQuery { get; set; }
    }
}
