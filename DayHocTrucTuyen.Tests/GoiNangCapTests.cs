using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class GoiNangCapTests
    {
        [Theory]
        [InlineData(1, "1 tháng")]
        [InlineData(6, "6 tháng")]
        [InlineData(11, "11 tháng")]
        public void GetHieuLuc_LessThanTwelveMonths_ReturnsMonthsFormat(int hieuLuc, string expected)
        {
            var goi = new GoiNangCap { HieuLuc = hieuLuc };

            var result = goi.getHieuLuc();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(12, "1 năm")]
        [InlineData(24, "2 năm")]
        [InlineData(36, "3 năm")]
        public void GetHieuLuc_ExactYearsWithoutRemainder_ReturnsYearsFormat(int hieuLuc, string expected)
        {
            var goi = new GoiNangCap { HieuLuc = hieuLuc };

            var result = goi.getHieuLuc();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(18, "1,6 năm")]
        [InlineData(25, "2,1 năm")]
        public void GetHieuLuc_YearsWithRemainingMonths_ReturnsYearsAndMonthsFormat(int hieuLuc, string expected)
        {
            var goi = new GoiNangCap { HieuLuc = hieuLuc };

            var result = goi.getHieuLuc();

            Assert.Equal(expected, result);
        }
    }
}
