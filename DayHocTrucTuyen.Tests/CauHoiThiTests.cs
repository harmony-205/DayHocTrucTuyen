using DayHocTrucTuyen.Models.Entities;
using Xunit;

namespace DayHocTrucTuyen.Tests
{
    public class CauHoiThiTests
    {
        [Fact]
        public void IsMultiAns_SingleAnswer_ReturnsFalse()
        {
            var cauHoi = new CauHoiThi();
            const string singleAns = "Lập trình C#";

            var result = cauHoi.isMultiAns(singleAns);

            Assert.False(result);
        }

        [Fact]
        public void IsMultiAns_MultipleAnswersSeparatedByBackslash_ReturnsTrue()
        {
            var cauHoi = new CauHoiThi();
            const string multiAns = "Lập trình C#\\Lập trình Java";

            var result = cauHoi.isMultiAns(multiAns);

            Assert.True(result);
        }

        [Fact]
        public void GetDapAnDung_SingleMatchingAnswerAtFirstPosition_ReturnsA()
        {
            var cauHoi = new CauHoiThi();
            const string da = "Đáp án 1\\Đáp án 2\\Đáp án 3\\Đáp án 4";
            const string lg = "Đáp án 1";

            var result = cauHoi.getDapAnDung(da, lg);

            Assert.Equal("A", result);
        }

        [Fact]
        public void GetDapAnDung_SingleMatchingAnswerAtThirdPosition_ReturnsC()
        {
            var cauHoi = new CauHoiThi();
            const string da = "Đáp án 1\\Đáp án 2\\Đáp án 3\\Đáp án 4";
            const string lg = "Đáp án 3";

            var result = cauHoi.getDapAnDung(da, lg);

            Assert.Equal("C", result);
        }

        [Fact]
        public void GetDapAnDung_MultipleMatchingAnswers_ReturnsCommaSeparatedLetters()
        {
            var cauHoi = new CauHoiThi();
            const string da = "Đáp án 1\\Đáp án 2\\Đáp án 3\\Đáp án 4";
            const string lg = "Đáp án 1\\Đáp án 3";

            var result = cauHoi.getDapAnDung(da, lg);

            Assert.Equal("A,C", result);
        }

        [Fact]
        public void GetDapAnDung_AllFourAnswersMatch_ReturnsAllLetters()
        {
            var cauHoi = new CauHoiThi();
            const string da = "A\\B\\C\\D";
            const string lg = "A\\B\\C\\D";

            var result = cauHoi.getDapAnDung(da, lg);

            Assert.Equal("A,B,C,D", result);
        }

        [Fact]
        public void GetDapAnDungAsInt_SingleMatchingAnswer_ReturnsZeroBasedIndexString()
        {
            var cauHoi = new CauHoiThi();
            const string da = "A\\B\\C\\D";
            const string lg = "B";

            var result = cauHoi.getDapAnDungAsInt(da, lg);

            Assert.Equal("1", result);
        }

        [Fact]
        public void GetDapAnDungAsInt_MultipleMatchingAnswers_ReturnsCommaSeparatedIndices()
        {
            var cauHoi = new CauHoiThi();
            const string da = "A\\B\\C\\D";
            const string lg = "A\\D";

            var result = cauHoi.getDapAnDungAsInt(da, lg);

            Assert.Equal("0,3", result);
        }

        [Theory]
        [InlineData(0, "A")]
        [InlineData(1, "B")]
        [InlineData(2, "C")]
        [InlineData(3, "D")]
        public void GetKyHieu_ValidIndicesZeroToThree_ReturnsCorrespondingLetter(int vitri, string expected)
        {
            var cauHoi = new CauHoiThi();

            var result = cauHoi.getKyHieu(vitri);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        [InlineData(99)]
        public void GetKyHieu_OutOfRangeIndex_ReturnsEmptyString(int vitri)
        {
            var cauHoi = new CauHoiThi();

            var result = cauHoi.getKyHieu(vitri);

            Assert.Equal("", result);
        }
    }
}
