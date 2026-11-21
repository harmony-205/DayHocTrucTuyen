using DayHocTrucTuyen.Areas.Admin.Models;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class PaginatedListAsyncTests
    {
        [Fact]
        public void HasPreviousPage_PageIndexEqualsOne_ReturnsFalse()
        {
            var list = new PaginatedListAsync<int>(new List<int> { 1, 2 }, count: 10, pageIndex: 1, pageSize: 2);

            Assert.False(list.HasPreviousPage);
        }

        [Fact]
        public void HasPreviousPage_PageIndexGreaterThanOne_ReturnsTrue()
        {
            var list = new PaginatedListAsync<int>(new List<int> { 3, 4 }, count: 10, pageIndex: 2, pageSize: 2);

            Assert.True(list.HasPreviousPage);
        }

        [Fact]
        public void HasNextPage_PageIndexLessThanTotalPages_ReturnsTrue()
        {
            var list = new PaginatedListAsync<int>(new List<int> { 1, 2 }, count: 10, pageIndex: 1, pageSize: 2);

            Assert.True(list.HasNextPage);
        }

        [Fact]
        public void HasNextPage_PageIndexEqualsTotalPages_ReturnsFalse()
        {
            var list = new PaginatedListAsync<int>(new List<int> { 9, 10 }, count: 10, pageIndex: 5, pageSize: 2);

            Assert.False(list.HasNextPage);
        }

        [Fact]
        public void Constructor_CountWithRemainder_CalculatesCeiledTotalPages()
        {
            var list = new PaginatedListAsync<int>(new List<int>(), count: 25, pageIndex: 1, pageSize: 10);

            Assert.Equal(3, list.TotalPages);
            Assert.Equal(25, list.TotalRecords);
            Assert.Equal(1, list.PageIndex);
            Assert.Equal(10, list.Pagesize);
        }
    }
}
