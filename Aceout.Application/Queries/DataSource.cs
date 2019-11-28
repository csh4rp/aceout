using System;
using System.Collections.Generic;
using System.Text;

namespace Aceout.Application.Queries
{
    public class DataSource<TModel>
    {
        public IEnumerable<TModel> Data { get; set; }
        public int RowCount { get; set; }
    }
}
