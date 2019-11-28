using Aceout.Tools.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Aceout.Web.Model
{
    public class Paginator
    {
        public int PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortBy { get; set; }

        public Paginator()
        {
        }

        public Pager<TModel> GetPager<TModel>()
        {
            var sortExpressions = GetSortExpressions<TModel>();

            var pageNumber = PageNumber > 0 ? PageNumber - 1 : 0;
            var pageSize = PageSize > 0 ? PageSize : null;

            return new Pager<TModel>
            {
                PageNumber = PageNumber,
                PageSize = PageSize,
                SortExpressions = sortExpressions
            };
        }

        private IEnumerable<SortExpression<TModel>> GetSortExpressions<TModel>()
        {
            if (!string.IsNullOrEmpty(SortBy))
            {
                var propNames = SortBy.Split(',');
                var properties = typeof(TModel).GetProperties();

                foreach (var propName in propNames)
                {
                    var name = propName.Replace("-", "");
                    var prop = properties.First(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                   
                    var param = System.Linq.Expressions.Expression.Parameter(typeof(TModel));
                    var expression = Expression.Property(param, prop);

                    var exp = Expression.Convert(expression, typeof(object));
                    var lambda =  System.Linq.Expressions.Expression.Lambda<Func<TModel, object>>(exp, param);


                    var direction = SortDirection.Asc;

                    if (propName.StartsWith("-"))
                    {
                        direction = SortDirection.Desc;
                    }

                    yield return new SortExpression<TModel>
                    {
                        Direction = direction,
                        Expression = lambda
                    };
                }
            }
        }


    }


}
