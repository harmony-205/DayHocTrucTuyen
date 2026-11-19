using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class PhongThiTests
    {
        [Fact]
        public void MahoaMatKhau_ComputesConsistentMd5Hex()
        {
            // Arrange
            var room = new PhongThi();
            const string roomPass = "exam2026";

            // Act
            var hash1 = room.mahoaMatKhau(roomPass);
            var hash2 = room.mahoaMatKhau(roomPass);

            // Assert
            Assert.NotNull(hash1);
            Assert.Equal(32, hash1.Length);
            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void MahoaMatKhau_MatchesNguoiDungHash()
        {
            // Both PhongThi and NguoiDung use the same MD5 hashing algorithm
            var room = new PhongThi();
            var user = new NguoiDung();
            const string secret = "securePass!@#";

            var roomHash = room.mahoaMatKhau(secret);
            var userHash = user.mahoaMatKhau(secret);

            Assert.Equal(roomHash, userHash);
        }
    }
}
