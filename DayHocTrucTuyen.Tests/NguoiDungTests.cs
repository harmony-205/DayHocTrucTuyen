using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class NguoiDungTests
    {
        [Fact]
        public void GetFullName_CombinesHoLotAndTen()
        {
            // Arrange
            var user = new NguoiDung
            {
                HoLot = "Dương",
                Ten = "Việt"
            };

            // Act
            var fullName = user.getFullName();

            // Assert
            Assert.Equal("Dương Việt", fullName);
        }

        [Fact]
        public void GetName_ReturnsTen()
        {
            // Arrange
            var user = new NguoiDung
            {
                Ten = "Admin"
            };

            // Act
            var name = user.getName();

            // Assert
            Assert.Equal("Admin", name);
        }

        [Fact]
        public void MahoaMatKhau_ComputesConsistentMd5Hash()
        {
            // Arrange
            var user = new NguoiDung();
            const string input = "123456";
            // MD5 of "123456" in uppercase hex: E10ADC3949BA59ABBE56E057F20F883E
            const string expectedHash = "E10ADC3949BA59ABBE56E057F20F883E";

            // Act
            var hash1 = user.mahoaMatKhau(input);
            var hash2 = user.mahoaMatKhau(input);

            // Assert
            Assert.Equal(expectedHash, hash1);
            Assert.Equal(hash1, hash2);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetImageAvt_WhenNullOrEmpty_ReturnsDefaultPath(string? avatar)
        {
            // Arrange
            var user = new NguoiDung
            {
                ImgAvt = avatar
            };

            // Act
            var path = user.getImageAvt();

            // Assert
            Assert.Equal("/Content/Img/userAvt/avt-default.png", path);
        }

        [Fact]
        public void GetImageAvt_WhenSpecified_PrependsResourcePath()
        {
            // Arrange
            var user = new NguoiDung
            {
                ImgAvt = "user-123.jpg"
            };

            // Act
            var path = user.getImageAvt();

            // Assert
            Assert.Equal("/Content/Img/userAvt/user-123.jpg", path);
        }
    }
}
