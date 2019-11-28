using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aceout.WebUI.Areas.Cms.Models
{
    public class CreateInforationModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public IEnumerable<int> GroupIds { get; set; }
    }
}
