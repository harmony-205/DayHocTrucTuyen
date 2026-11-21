using DayHocTrucTuyen.Models.Payment.Momo;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class MoMoSecurityTests
    {
        [Fact]
        public void SignSHA256_ValidMessageAndSecretKey_ReturnsValidLowercaseHexHash()
        {
            var crypto = new MoMoSecurity();
            const string message = "partnerCode=MOMO&amount=10000";
            const string key = "secret123";

            var hash = crypto.signSHA256(message, key);

            Assert.NotNull(hash);
            Assert.Equal(64, hash.Length);
            Assert.DoesNotContain("-", hash);
            Assert.Equal(hash.ToLower(), hash);
        }

        [Fact]
        public void SignSHA256_RepeatedWithSameInputs_ReturnsDeterministicHash()
        {
            var crypto = new MoMoSecurity();
            const string message = "orderId=123456&amount=50000";
            const string key = "testKeyXYZ";

            var hash1 = crypto.signSHA256(message, key);
            var hash2 = crypto.signSHA256(message, key);

            Assert.Equal(hash1, hash2);
        }
    }
}
