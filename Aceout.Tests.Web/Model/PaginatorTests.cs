using Aceout.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aceout.Tools.Data;
using Xunit;

namespace Aceout.Tests.Web.Model
{
    public class PaginatorTests
    {
        IQueryable<SampleModel> GetFakeData()
        {
            return new List<SampleModel>
            {
                new SampleModel
                {
                    Id = 2,
                    Name = "A",
                    Value = 1.12m
                },
                new SampleModel
                {
                    Id = 4,
                    Name = "A",
                    Value = 1.15m
                },
                new SampleModel
                {
                    Id = 1,
                    Name = "B",
                    Value = 1m
                },
                new SampleModel
                {
                    Id = 3,
                    Name = "C",
                    Value = 0.9m
                },
                new SampleModel
                {
                    Id = 5,
                    Name = "D",
                    Value = 0.9m
                }
            }.AsQueryable();
        }

        [Fact]
        public void SortPaginator_SimpleSort_CanSort()
        {
            var paginator = new Paginator
            {
                SortBy = "Name"
            };

            var sorted = GetFakeData().Paginate<SampleModel>(paginator.GetPager<SampleModel>());

            Assert.Equal("A", sorted.First().Name);
        }

        [Fact]
        public void SortPaginator_MultiSort_CanSort()
        {
            var paginator = new Paginator
            {
                SortBy = "Name,Id"
            };

            var sorted = GetFakeData().Paginate<SampleModel>(paginator.GetPager<SampleModel>());

            Assert.Equal(2, sorted.First().Id);
        }

        [Fact]
        public void SortPaginator_MultiSortDIfferentWays_CanSort()
        {
            var paginator = new Paginator
            {
                SortBy = "Name,-Id"
            };

            var sorted = GetFakeData().Paginate<SampleModel>(paginator.GetPager<SampleModel>());

            Assert.Equal(4, sorted.First().Id);
        }

        [Theory]
        [InlineData(0, 2, 1, 2)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 4, 5, 1)]
        public void SortPaginator_SimplePaging_CanSort(int page, int size, int expectedId, int expectedCount)
        {
            var paginator = new Paginator
            {
                PageSize = size,
                PageNumber = page,
                SortBy = "Id"
            };

            var sorted = GetFakeData().Paginate<SampleModel>(paginator.GetPager<SampleModel>());

            Assert.Equal(expectedCount, sorted.Count());
            Assert.Equal(expectedId, sorted.First().Id);
        }
    }

    class SampleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
    }
}
