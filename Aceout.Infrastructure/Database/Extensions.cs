using Aceout.Tools.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using NHibernate.Hql.Ast.ANTLR;
using NHibernate.Impl;
using NHibernate.Linq;
using NHibernate.Loader.Criteria;
using NHibernate.Persister.Entity;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aceout.Infrastructure.Database
{
    public static class Extensions
    {
        public static IList<K> SelectEntityList<T, K>(this NHibernate.IQueryOver<T, T> queryOver) where K : new()
        {
            queryOver = queryOver.SelectList(x => x.SelectEntity<T, K>());
            queryOver = queryOver.TransformUsing(new EntityTransformer<K>());
            return queryOver.List<K>();
        }

        public static Task<IList<K>> SelectEntityListAsync<T, K>(this NHibernate.IQueryOver<T, T> queryOver, CancellationToken cancellationToken = default(CancellationToken)) where K : new()
        {
            queryOver = queryOver.SelectList(x => x.SelectEntity<T, K>());
            queryOver = queryOver.TransformUsing(new EntityTransformer<K>());
            return queryOver.ListAsync<K>();
        }

        public static NHibernate.IQueryOver<TQueryModel, TQueryModel> SelectEntity<TQueryModel, KResult>(this NHibernate.IQueryOver<TQueryModel, TQueryModel> queryOver) where KResult : new()
        {
            queryOver = queryOver.SelectList(x => x.SelectEntity<TQueryModel, KResult>());
            return queryOver.TransformUsing(new EntityTransformer<KResult>());
        }

        public static QueryOverProjectionBuilder<T> SelectEntity<T, K>(this QueryOverProjectionBuilder<T> query)
        {
            var properties = typeof(K).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => x.CanWrite)
                .OrderBy(x => x.Name)
                .Select(x => x.Name)
                .ToList();

            foreach (var prop in properties)
            {
                query = query.Select(Projections.Property(prop));
            }

            return query;
        }

        public static IList<T> ListOf<T>(this IQueryOver query, T result)
        {
            var ctor = typeof(T).GetConstructors().First();
            return query.UnderlyingCriteria.SetResultTransformer(Transformers.AliasToBeanConstructor(ctor))
                .List<T>();
        }

        public static Task<IList<T>> ListOfAsync<T>(this IQueryOver query, T result, CancellationToken cancellationToken = default(CancellationToken))
        {
            var ctor = typeof(T).GetConstructors().First();
            return query.UnderlyingCriteria.SetResultTransformer(Transformers.AliasToBean<T>())
                .ListAsync<T>(cancellationToken);
        }

        public static IQueryOver<TModel, TModel> Paginate<TModel>(this IQueryOver<TModel, TModel> query, Pager<TModel> pager)
        {
            var first = true;
            var orderedQuery = (IQueryOver<TModel, TModel>)null;

            foreach (var sortExpression in pager.SortExpressions)
            {
                if (first)
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = query.OrderBy(sortExpression.Expression).Asc;
                    }
                    else
                    {
                        orderedQuery = query.OrderBy(sortExpression.Expression).Desc;
                    }

                    first = false;
                }
                else
                {
                    if (sortExpression.Direction == SortDirection.Asc)
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpression.Expression).Asc;
                    }
                    else
                    {
                        orderedQuery = orderedQuery.ThenBy(sortExpression.Expression).Desc;
                    }
                }
            }

            if (orderedQuery != null)
            {
                if(pager.PageSize > 0)
                {
                    orderedQuery.RootCriteria.SetFirstResult(pager.Offset.Value)
                        .SetMaxResults(pager.PageSize.Value);
                }

                return orderedQuery;
            }
            else if(pager.PageSize > 0)
            {
                query.RootCriteria.SetFirstResult(pager.Offset.Value)
                    .SetMaxResults(pager.PageSize.Value);
            }

            return query;
        }

        public static String ToSql(this System.Linq.IQueryable queryable)
        {
            var sessionProperty = typeof(DefaultQueryProvider).GetProperty("Session", BindingFlags.NonPublic | BindingFlags.Instance);
            var session = sessionProperty.GetValue(queryable.Provider, null) as ISession;
            var sessionImpl = session.GetSessionImplementation();
            var factory = sessionImpl.Factory;
            var nhLinqExpression = new NhLinqExpression(queryable.Expression, factory);
            var translatorFactory = new ASTQueryTranslatorFactory();
            var translator = translatorFactory.CreateQueryTranslators(nhLinqExpression, null, false, sessionImpl.EnabledFilters, factory).First();
            //in case you want the parameters as well
            //var parameters = nhLinqExpression.ParameterValuesByName.ToDictionary(x => x.Key, x => x.Value.Item1);

            return translator.SQLString;
        }

        public static String GetGeneratedSql(this ICriteria criteria)
        {
            var criteriaImpl = (CriteriaImpl)criteria;
            var sessionImpl = (SessionImpl)criteriaImpl.Session;
            var factory = (SessionFactoryImpl)sessionImpl.SessionFactory;
            var implementors = factory.GetImplementors(criteriaImpl.EntityOrClassName);
            var loader = new CriteriaLoader((IOuterJoinLoadable)factory.GetEntityPersister(implementors[0]), factory, criteriaImpl, implementors[0], sessionImpl.EnabledFilters);

            return loader.SqlString.ToString();
        }
    }
}
