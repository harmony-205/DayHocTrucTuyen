using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class ThongBaoTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GetLienKet_NullOrEmptyLienKet_ReturnsHashSymbol(string? link)
        {
            var tb = new ThongBao { LienKet = link };

            var result = tb.getLienKet();

            Assert.Equal("#", result);
        }

        [Fact]
        public void GetLienKet_ValidPath_PrependsForwardSlash()
        {
            var tb = new ThongBao { LienKet = "courses/room/detail/100" };

            var result = tb.getLienKet();

            Assert.Equal("/courses/room/detail/100", result);
        }

        [Theory]
        [InlineData("exam", "fa-edit")]
        [InlineData("post", "fa-comments")]
        [InlineData("money", "fa-money")]
        [InlineData("admin", "fa-user-secret")]
        public void GetIcon_KnownNotificationTypes_ReturnsMappedFontAwesomeClass(string loaiTb, string expectedIcon)
        {
            var tb = new ThongBao { LoaiTb = loaiTb };

            var result = tb.getIcon();

            Assert.Equal(expectedIcon, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("unknown_type")]
        public void GetIcon_UnknownOrNullType_ReturnsDefaultBellIcon(string? loaiTb)
        {
            var tb = new ThongBao { LoaiTb = loaiTb! };

            var result = tb.getIcon();

            Assert.Equal("fa-bell", result);
        }

        [Theory]
        [InlineData("exam", "Content/Img/Resources/notification-exam.png")]
        [InlineData("post", "Content/Img/Resources/notification-post.png")]
        [InlineData("money", "Content/Img/Resources/notification-money.png")]
        [InlineData("admin", "Content/Img/Resources/notification-admin.png")]
        public void GetIconImg_KnownNotificationTypes_ReturnsMappedImagePath(string loaiTb, string expectedImage)
        {
            var tb = new ThongBao { LoaiTb = loaiTb };

            var result = tb.getIconImg();

            Assert.Equal(expectedImage, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("other")]
        public void GetIconImg_UnknownOrNullType_ReturnsDefaultImage(string? loaiTb)
        {
            var tb = new ThongBao { LoaiTb = loaiTb! };

            var result = tb.getIconImg();

            Assert.Equal("Content/Img/Resources/notification-default.png", result);
        }
    }
}
