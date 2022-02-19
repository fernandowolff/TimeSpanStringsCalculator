using TimeSpanStringsCalculator.Models;
using Xunit;

namespace TimeSpanStringsCalculator.Tests
{
    public class Convertion
    {
        [Theory]
        [InlineData("5")]
        public void SingleTimeSpanWithNoTimeFlagResultsInException(string timeSpan)
        {
            var converter = new Converter();

            var invalidFormatIdentification = () =>
            {
                converter.SumTimeSpansToHours(new string[] { timeSpan });
            };

            Assert.Throws<InvalidTimeSpanFormatException>(invalidFormatIdentification);
        }

        [Theory]
        [InlineData("6h")]
        public void SingleTimeSpanWithHoursFlagResultsInSameValueConvertedToDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(6M, timeSpanInHours);
        }

        [Theory]
        [InlineData("8h48m")]
        public void SingleTimeSpanWithHoursAndMinutesFlagsResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(8.8M, timeSpanInHours);
        }

        [Theory]
        [InlineData("8h30m30s")]
        public void SingleTimeSpanWithHoursMinutesAndSecondsFlagsResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(8.5083M, timeSpanInHours, 4);
        }
        
        [Theory]
        [InlineData("30d")]
        public void SingleTimeSpanWithDaysFlagResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(720M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData(724, "30d4h")]
        public void SingleTimeSpanWithDaysAndHoursFlagsResultsInDecimalHours(decimal expected, string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(expected, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("30m")]
        public void SingleTimeSpanWithMinutesFlagResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(0.5M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("30s")]
        public void SingleTimeSpanWithSecondsFlagResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(0.0083M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("30m30s")]
        public void SingleTimeSpanWithMinutesAndSecondsFlagsResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(0.5083M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("30h30s")]
        public void SingleTimeSpanWithHoursAndSecondsFlagsResultsInDecimalHours(string timeSpan)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(new[] { timeSpan });

            Assert.Equal(30.0083M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("2d", "2h", "1h30min", "30s", "5h", "4h")]
        public void MultipleTimeSpansShouldReturnTheTotalSumInDecimalHours(params string[] timeSpans)
        {
            var converter = new Converter();

            var timeSpanInHours = converter.SumTimeSpansToHours(timeSpans);

            Assert.Equal(60.5083M, timeSpanInHours, 4);
        }

        [Theory]
        [InlineData("2fsdf", "1h3tgmin", "fsdf", "5h", "4")]
        public void MultipleTimeSpansWithIncorrectFormatShouldThrowException(params string[] timeSpans)
        {
            var converter = new Converter();

            var invalidFormatIdentification = () =>
            {
                converter.SumTimeSpansToHours(timeSpans);
            };

            Assert.Throws<InvalidTimeSpanFormatException>(invalidFormatIdentification);
        }

        [Theory]
        [InlineData("2h", "1h30min", "30s", "5h32s", "4m", "3m45sec")]
        public void MultipleTimeSpansShouldReturnTheTotalSumInDecimalMinutes(params string[] timeSpans)
        {
            var converter = new Converter();

            var timeSpanInMinutes = converter.SumTimeSpansToMinutes(timeSpans);

            Assert.Equal(518.7833M, timeSpanInMinutes, 4);
        }

        [Theory]
        [InlineData("1d", "2h", "1h30min", "30s", "5h32s", "4m", "3m45sec")]
        public void MultipleTimeSpansShouldReturnTheTotalSumInDecimalSeconds(params string[] timeSpans)
        {
            var converter = new Converter();

            var timeSpanInSeconds = converter.SumTimeSpansToSeconds(timeSpans);

            Assert.Equal(117527M, timeSpanInSeconds, 4);
        }

        [Theory]
        [InlineData("8h38m47s", new string[] { "2h", "1h30min", "30s", "5h32s", "4m", "3m45sec" })]
        [InlineData("9h", new string[] { "15m", "15m", "1h", "1h", "1h30m", "1h", "15m", "15m", "1h", "15m", "1h30m", "45m" })]
        [InlineData("15m", new string[] { "15m" })]
        [InlineData("30s", new string[] { "0h30s" })]
        [InlineData("3h30s", new string[] { "3hours30s" })]
        [InlineData("1d3h30s", new string[] { "1d3hours30s" })]
        public void MultipleTimeSpansShouldReturnTheTotalSumFormatted(string expected, string[] timeSpans)
        {
            var converter = new Converter();

            var formattedTimeSpan = converter.GetTimeSpansSumFormatted(timeSpans);

            Assert.Equal(expected, formattedTimeSpan);
        }
    }
}