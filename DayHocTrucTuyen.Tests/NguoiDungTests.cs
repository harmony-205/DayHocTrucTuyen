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

        [Fact]
        public void GetTuoi_BirthdayAlreadyOccurredThisYear_ReturnsExactAge()
        {
            var now = DateTime.Now;
            var user = new NguoiDung
            {
                NgaySinh = new DateTime(now.Year - 20, 1, 1)
            };

            var age = user.getTuoi();

            Assert.Equal(20, age);
        }

        [Fact]
        public void GetTuoi_BirthdayHasNotOccurredThisYear_ReturnsAgeMinusOne()
        {
            var now = DateTime.Now;
            var tomorrow20YearsAgo = now.AddYears(-20).AddDays(1);
            var user = new NguoiDung
            {
                NgaySinh = tomorrow20YearsAgo
            };

            var age = user.getTuoi();

            Assert.Equal(19, age);
        }

        [Fact]
        public void GetTuoi_NullBirthDate_ReturnsZero()
        {
            var user = new NguoiDung { NgaySinh = null };

            var age = user.getTuoi();

            Assert.Equal(0, age);
        }

        [Theory]
        [InlineData(1, "Nam")]
        [InlineData(2, "Nữ")]
        [InlineData(3, "Khác")]
        public void GetGioiTinh_ValidGenderCodes_ReturnsRespectiveText(int genderCode, string expected)
        {
            var user = new NguoiDung { GioiTinh = genderCode };

            var result = user.getGioiTinh();

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(4)]
        public void GetGioiTinh_NullOrUnknownGenderCode_ReturnsDefaultText(int? genderCode)
        {
            var user = new NguoiDung { GioiTinh = genderCode };

            var result = user.getGioiTinh();

            Assert.Equal("Chưa chọn giới tính", result);
        }

        [Fact]
        public void GetIntro_AdminRole_ReturnsAdminDescription()
        {
            var user = new NguoiDung { MaLoai = "01" };

            var intro = user.getIntro();

            Assert.Contains("quản lý", intro);
        }

        [Fact]
        public void GetIntro_TeacherRole_ReturnsTeacherDescription()
        {
            var user = new NguoiDung { MaLoai = "02" };

            var intro = user.getIntro();

            Assert.Contains("Lực lượng nòng cốt", intro);
        }

        [Theory]
        [InlineData("03")]
        [InlineData(null)]
        [InlineData("other")]
        public void GetIntro_StudentOrOtherRole_ReturnsBasicUserDescription(string? role)
        {
            var user = new NguoiDung { MaLoai = role };

            var intro = user.getIntro();

            Assert.Contains("Người dùng cơ bản", intro);
        }

        [Fact]
        public void GetImageBG_NullBackground_ReturnsDefaultPath()
        {
            var user = new NguoiDung { ImgBg = null };

            var result = user.getImageBG();

            Assert.Equal("/Content/Img/userBG/bg-default.jpg", result);
        }

        [Fact]
        public void GetImageBG_ExternalHttpUrl_ReturnsOriginalUrl()
        {
            const string externalUrl = "https://images.example.com/bg.png";
            var user = new NguoiDung { ImgBg = externalUrl };

            var result = user.getImageBG();

            Assert.Equal(externalUrl, result);
        }

        [Fact]
        public void GetImageBG_LocalFilename_PrependsResourceDirectory()
        {
            var user = new NguoiDung { ImgBg = "mybg.jpg" };

            var result = user.getImageBG();

            Assert.Equal("/Content/Img/userBG/mybg.jpg", result);
        }
    }
}
