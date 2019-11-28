using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Aceout.Tools.Data
{
    public class Pager<TModel>
    {
        public int? PageSize { get; set; }
        public int PageNumber { get; set; }
        public int? Offset => PageNumber * PageSize;
        public IEnumerable<SortExpression<TModel>> SortExpressions { get; set; }

    }

    public class SortExpression<TModel>
    {
        public Expression<Func<TModel, object>> Expression { get; set; }
        public SortDirection Direction { get; set; }
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

}
