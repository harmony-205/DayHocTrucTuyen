using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class LopHocTests
    {
        [Fact]
        public void GetGiaTien_WhenZero_ReturnsMienPhi()
        {
            // Arrange
            var lop = new LopHoc { GiaTien = 0 };

            // Act
            var result = lop.getGiaTien();

            // Assert
            Assert.Equal("Miễn phí", result);
        }

        [Fact]
        public void GetGiaTien_WhenPositive_ReturnsFormattedVnd()
        {
            // Arrange
            var lop = new LopHoc { GiaTien = 150000 };

            // Act
            var result = lop.getGiaTien();

            // Assert
            Assert.Equal("150,000 VNĐ", result);
        }

        [Theory]
        [InlineData(0, "price-free")]
        [InlineData(10000, "price-cheap")]
        [InlineData(49999, "price-cheap")]
        [InlineData(50000, "price-medium")]
        [InlineData(99999, "price-medium")]
        [InlineData(100000, "price-expensive")]
        [InlineData(299999, "price-expensive")]
        [InlineData(300000, "price-extreme")]
        [InlineData(1000000, "price-extreme")]
        public void GetTypeGiaTien_BoundaryValues_ReturnsCorrectBadgeClass(int giaTien, string expectedClass)
        {
            // Arrange
            var lop = new LopHoc { GiaTien = giaTien };

            // Act
            var badgeClass = lop.getTypeGiaTien();

            // Assert
            Assert.Equal(expectedClass, badgeClass);
        }
    }
}
