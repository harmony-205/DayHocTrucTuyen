using DayHocTrucTuyen.Models;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class StringGeneratorTests
    {
        [Fact]
        public void Alphabet_PositiveLengthRequested_ReturnsExactStringLength()
        {
            const int length = 15;

            var result = StringGenerator.Alphabet(length);

            Assert.Equal(length, result.Length);
        }

        [Fact]
        public void Alphabet_ValidCharacterSet_ContainsOnlyLetters()
        {
            const int length = 50;

            var result = StringGenerator.Alphabet(length);

            Assert.All(result, c => Assert.True(char.IsLetter(c)));
        }

        [Fact]
        public void Alphabet_ZeroLengthRequested_ReturnsEmptyString()
        {
            var result = StringGenerator.Alphabet(0);

            Assert.Equal("", result);
        }
    }
}
