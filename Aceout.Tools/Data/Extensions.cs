using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aceout.Tools.Data
{
    public static class Extensions
    {
        public static IQueryable<TModel> Paginate<TModel>(this IQueryable<TModel> query, Pager<TModel> pager)
        {
            var first = true;
            var orderedQuery = (IOrderedQueryable<TModel>)null;

            foreach (var sortExpression in pager.SortExpressions)
            {
                if (first)
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = query.OrderBy(sortExpression.Expression);
                    }
                    else
                    {
                        orderedQuery = query.OrderByDescending(sortExpression.Expression);
                    }

                    first = false;
                }
                else
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpression.Expression);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(sortExpression.Expression);
                    }
                }
            }

            if (orderedQuery != null)
            {
                if(pager.PageSize > 0)
                {
                    return orderedQuery.Skip(pager.Offset.Value)
                        .Take(pager.PageSize.Value)
                        .AsQueryable();
                }
                else
                {
                    return orderedQuery.AsQueryable();
                }
            }
            else
            {
                if(pager.PageSize > 0)
                {
                    return query.Skip(pager.Offset.Value)
                        .Take(pager.PageSize.Value)
                        .AsQueryable();
                }
                else
                {
                    return query.AsQueryable();
                }
            }
        }

 

        public static DataSource<TModel> ToDataSource<TModel>(this IQueryable<TModel> query, Pager<TModel> pager)
        {
            var first = true;
            var orderedQuery = (IOrderedQueryable<TModel>)null;
            var dataSource = new DataSource<TModel>();

            foreach (var sortExpression in pager.SortExpressions)
            {
                if (first)
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = query.OrderBy(sortExpression.Expression);
                    }
                    else
                    {
                        orderedQuery = query.OrderByDescending(sortExpression.Expression);
                    }

                    first = false;
                }
                else
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpression.Expression);
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenByDescending(sortExpression.Expression);
                    }
                }
            }

            if (orderedQuery != null)
            {
                if (pager.PageSize > 0)
                {
                    dataSource.Data = orderedQuery.Skip(pager.Offset.Value)
                        .Take(pager.PageSize.Value)
                        .AsQueryable();
                }
                else
                {
                    dataSource.Data = orderedQuery.AsQueryable();
                }

                dataSource.RowCount = orderedQuery.Count();
            }
            else
            {
                if (pager.PageSize > 0)
                {
                    dataSource.Data = query.Skip(pager.Offset.Value)
                        .Take(pager.PageSize.Value)
                        .AsQueryable();
                }
                else
                {
                    dataSource.Data = query.AsQueryable();
                }

                dataSource.RowCount = query.Count();
            }

            return dataSource;
        }
    }
}
