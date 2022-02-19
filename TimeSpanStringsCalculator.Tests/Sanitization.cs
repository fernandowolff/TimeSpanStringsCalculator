using TimeSpanStringsCalculator.Models;
using Xunit;

namespace TimeSpanStringsCalculator.Tests
{
    public class Sanitization
    {
        [Theory]
        [InlineData("4242dfsdfsdf")]
        public void SingleTimeSpanOnWrongFormatShouldThrowError(string timeSpan)
        {
            var hoursConverter = new Converter();

            var invalidFormatIdentification = () =>
            {
                hoursConverter.SumTimeSpansToHours(new[] { timeSpan });
            };

            Assert.Throws<InvalidTimeSpanFormatException>(invalidFormatIdentification);
        }

        [Theory]
        [InlineData("8hours30minutes30seconds", "856hour48min37sec", "30m32s", "48seconds", "76min", "8")]
        public void TimeSpansWithExtensiveTimeTagsShouldBeSanitizedToSimpleTags(params string[] timeSpans)
        {
            var hoursConverter = new Converter();
            var sanitizationResults = new string[timeSpans.Length];

            for(var i = 0; i < timeSpans.Length; i++)
            {
                sanitizationResults[i] = hoursConverter.SanitizeTimeSpanString(timeSpans[i]);
            }

            Assert.Equal("8h30m30s", sanitizationResults[0]);
            Assert.Equal("856h48m37s", sanitizationResults[1]);
            Assert.Equal("30m32s", sanitizationResults[2]);
            Assert.Equal("48s", sanitizationResults[3]);
            Assert.Equal("76m", sanitizationResults[4]);
            Assert.Equal("8", sanitizationResults[5]);
        }
    }
}
