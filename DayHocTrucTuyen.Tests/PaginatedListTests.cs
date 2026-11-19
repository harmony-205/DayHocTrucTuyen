using DayHocTrucTuyen.Areas.Admin.Models;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class PaginatedListTests
    {
        [Fact]
        public void Create_CalculatesTotalPages_Correctly()
        {
            // Arrange: 25 items, pageSize = 10 -> 3 pages
            var source = Enumerable.Range(1, 25).ToList();

            // Act
            var result = PaginatedList<int>.Create(source, pageOffset: 0, pageSize: 10);

            // Assert
            Assert.Equal(3, result.TotalPages);
            Assert.Equal(25, result.TotalRecords);
            Assert.Equal(10, result.Pagesize);
            Assert.Equal(0, result.PageOffset);
            Assert.Equal(10, result.Count);
            Assert.Equal(1, result[0]);
            Assert.Equal(10, result[9]);
        }

        [Fact]
        public void Create_WithExactMultiple_CalculatesExactPages()
        {
            // Arrange: 30 items, pageSize = 10 -> exactly 3 pages
            var source = Enumerable.Range(1, 30).ToList();

            // Act
            var result = PaginatedList<int>.Create(source, pageOffset: 0, pageSize: 10);

            // Assert
            Assert.Equal(3, result.TotalPages);
            Assert.Equal(30, result.TotalRecords);
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void Create_WithEmptySource_ReturnsZeroPagesAndRecords()
        {
            // Arrange: empty source
            var source = new List<string>();

            // Act
            var result = PaginatedList<string>.Create(source, pageOffset: 0, pageSize: 10);

            // Assert
            Assert.Equal(0, result.TotalPages);
            Assert.Equal(0, result.TotalRecords);
            Assert.Empty(result);
        }

        [Fact]
        public void Create_AppliesSkipAndTake_Correctly()
        {
            // Arrange: 50 items, offset = 20, pageSize = 10 -> items 21..30
            var source = Enumerable.Range(1, 50).ToList();

            // Act
            var result = PaginatedList<int>.Create(source, pageOffset: 20, pageSize: 10);

            // Assert
            Assert.Equal(20, result.PageOffset);
            Assert.Equal(10, result.Count);
            Assert.Equal(21, result[0]);
            Assert.Equal(30, result[9]);
        }

        [Fact]
        public void Create_SinglePage_WhenCountLessThanPageSize()
        {
            // Arrange: 5 items, pageSize = 10
            var source = Enumerable.Range(1, 5).ToList();

            // Act
            var result = PaginatedList<int>.Create(source, pageOffset: 0, pageSize: 10);

            // Assert
            Assert.Equal(1, result.TotalPages);
            Assert.Equal(5, result.TotalRecords);
            Assert.Equal(5, result.Count);
        }
    }
}
