using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Aceout.Application.Queries.Lms.Categories.Handlers;
using Aceout.Application.Queries.Lms.Categories.Models;
using Aceout.Application.Queries.Lms.Categories.Results;
using Aceout.Domain.Model.Lms;
using Aceout.Test.Utils;
using Aceout.Tests.Utils;
using Aceout.Tools.Data;
using NHibernate;
using Xunit;

namespace Aceout.Tests.Application.Queries.Lms.MaterialCategories
{
    public class MaterialCategoryDataSourceQueryTests
    {
        private ISession _session;
        private Pager<MaterialCategoryDto> _pager;

        public MaterialCategoryDataSourceQueryTests()
        {
            _pager = new Pager<MaterialCategoryDto>()
            {
                SortExpressions = Enumerable.Empty<SortExpression<MaterialCategoryDto>>(),
                PageSize = 20
            };

            _session = SessionProvider.GetSession();
        }

        public static void Prepare(ISession session)
        {

            var result = session.CreateSQLQuery(SchemaScripts.MaterialCategory)
                .ExecuteUpdate();

            session.Flush();

            var categories = new[]
            {
                new MaterialCategory("Category1", "pl")
                {
                    Id = 1
                },
                new MaterialCategory("Category2", "pl")
                {
                    Id = 2
                },
                new MaterialCategory("Category3", "pl")
                {
                    Id =  3
                },
                new MaterialCategory("Category4", "en")
                {
                    Id =  4
                },
            };

            foreach (var category in categories)
            {
                session.Save(category);
            }

            session.Flush();

        }

        [Theory]
        [InlineData(0, 2, 4, 2)]
        [InlineData(0, 1, 4, 1)]
        [InlineData(1, 3, 4, 1)]
        public void QueryDataSource_ReturnsItems_Different(int page, int rowCount, int total, int returned)
        {
            _pager.PageSize = rowCount;
            _pager.PageNumber = page;

            using (var transaction = _session.BeginTransaction())
            {
                Prepare(_session);

                var query = new MaterialCategoryDataSourceQuery
                {
                    Pager = _pager,
                };

                var handler = new MaterialCategoryDataSourceQueryHandler(_session);

                var result = handler.Handle(query, default(CancellationToken)).Result;

                Assert.Equal(total, result.RowCount);
                Assert.Equal(returned, result.Data.Count());
            }

        }
    }
}
