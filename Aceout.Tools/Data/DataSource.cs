using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Tools.Data
{
    public class DataSource<TModel>
    {
        public int RowCount { get; set; }
        public IEnumerable<TModel> Data { get; set; }
    }
}
