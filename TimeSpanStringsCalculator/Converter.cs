using System.Text.RegularExpressions;
using TimeSpanStringsCalculator.Extensions;
using TimeSpanStringsCalculator.Models;

namespace TimeSpanStringsCalculator
{
    public class Converter
    {
        public const string TimeSpanFormat = "^(\\d*(days|day|d){1})?(\\d*(h|hours|hour){1})?(\\d*(m|min|minutes){1})?(\\d*(s|sec|seconds){1})?$";

        public decimal ConvertToSeconds(string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return 0;
            }

            this.IsTimeSpanFormatValid(timeSpan);

            timeSpan = this.SanitizeTimeSpanString(timeSpan);

            if (!timeSpan.Contains('d') && !timeSpan.Contains('h') && !timeSpan.Contains('m') && !timeSpan.Contains('s'))
            {
                return decimal.Parse(timeSpan) * 60 * 60;
            }

            var days = this.GetDaysSectionFromTimeSpan(timeSpan) * 24 * 60 * 60;

            var hours = this.GetHourSectionFromTimeSpan(timeSpan) * 60 * 60;

            var minutes = this.GetMinutesSectionFromTimeSpan(timeSpan) * 60;

            var seconds = this.GetSecondsSectionFromTimeSpan(timeSpan);

            return days + hours + minutes + seconds;
        }

        public decimal SumTimeSpansToDays(string[] timeSpans)
        {
            var hours = this.SumTimeSpansToHours(timeSpans);

            return hours / 24;
        }

        public decimal SumTimeSpansToHours(string[] timeSpans)
        {
            var minutes = this.SumTimeSpansToMinutes(timeSpans);

            return minutes / 60;
        }

        public decimal SumTimeSpansToMinutes(string [] timeSpans)
        {
            var seconds = this.SumTimeSpansToSeconds(timeSpans);

            return seconds / 60;
        }

        public decimal SumTimeSpansToSeconds(string[] timeSpans)
        {
            if (timeSpans == null || timeSpans.Length == 0)
            {
                return Decimal.Zero;
            }

            var seconds = decimal.Zero;

            foreach (var timeSpan in timeSpans)
            {
                seconds += this.ConvertToSeconds(timeSpan);
            }

            return seconds;
        }

        public string GetTimeSpansSumFormatted(string[] timeSpans)
        {
            if (timeSpans == null || timeSpans.Length == 0)
            {
                return "0h";
            }

            var seconds = this.SumTimeSpansToSeconds(timeSpans);

            var timeSpan = TimeSpan.FromSeconds((double)seconds);

            var formattedTime = GetFormattedTimeSpan(timeSpan);

            return formattedTime;
        }

        public string SanitizeTimeSpanString(string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return timeSpan;
            }

            return timeSpan
                .ToLower()
                .SanitizeDaysFlag()
                .SanitizeHoursFlag()
                .SanitizeMinutesFlag()
                .SanitizeSecondsFlag();
        }

        private bool IsTimeSpanFormatValid(string timeSpan)
        {
            if(Regex.IsMatch(timeSpan, TimeSpanFormat))
            {
                return true;
            }

            throw new InvalidTimeSpanFormatException(timeSpan);
        }

        private decimal GetDaysSectionFromTimeSpan(string timeSpan)
        {
            if (!timeSpan.Contains('d'))
            {
                return 0;
            }

            var dayFlagIndex = timeSpan.IndexOf('d');

            var dayString = timeSpan.Substring(0, dayFlagIndex);

            return decimal.Parse(dayString);
        }

        private decimal GetHourSectionFromTimeSpan(string timeSpan)
        {
            if (!timeSpan.Contains('h'))
            {
                return 0;
            }

            var daysFlagIndex = timeSpan.IndexOf('d');
            var hourFlagIndex = timeSpan.IndexOf('h');

            var hourString = timeSpan.Substring(daysFlagIndex + 1, hourFlagIndex - (daysFlagIndex + 1));

            return decimal.Parse(hourString);
        }

        private decimal GetMinutesSectionFromTimeSpan(string timeSpan)
        {
            if (!timeSpan.Contains('m'))
            {
                return 0;
            }

            var hourFlagIndex = timeSpan.IndexOf('h');
            var minutesFlagIndex = timeSpan.IndexOf('m');

            var minutesString = timeSpan.Substring(hourFlagIndex + 1, minutesFlagIndex - (hourFlagIndex + 1));

            return decimal.Parse(minutesString);
        }

        private decimal GetSecondsSectionFromTimeSpan(string timeSpan)
        {
            if (!timeSpan.Contains('s'))
            {
                return 0;
            }

            timeSpan = timeSpan.Replace("h", ";").Replace("m", ";").Replace("s", ";");
            var timeSpanPartitions = timeSpan.Split(";");

            var secondsString = timeSpanPartitions.Where(x => !string.IsNullOrEmpty(x)).Last();

            return decimal.Parse(secondsString);
        }

        private string GetFormattedTimeSpan(TimeSpan timeSpan)
        {
            var formattedTime = string.Empty;

            if(timeSpan.Days != 0)
            {
                formattedTime += $"{timeSpan.Days}d";
            }

            if (timeSpan.Hours != 0)
            {
                formattedTime += $"{timeSpan.Hours}h";
            }

            if (timeSpan.Minutes != 0)
            {
                formattedTime += $"{timeSpan.Minutes}m";
            }

            if (timeSpan.Seconds != 0)
            {
                formattedTime += $"{timeSpan.Seconds}s";
            }

            return formattedTime;
        }
    }
}