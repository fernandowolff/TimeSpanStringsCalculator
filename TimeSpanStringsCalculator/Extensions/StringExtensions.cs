namespace TimeSpanStringsCalculator.Extensions
{
    public static class StringExtensions
    {
        public static string SanitizeSecondsFlag(this string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return timeSpan;
            }

            return timeSpan
                .ToLower()
                .Replace("seconds", "s")
                .Replace("second", "s")
                .Replace("sec", "s");
        }

        public static string SanitizeMinutesFlag(this string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return timeSpan;
            }

            return timeSpan
                .ToLower()
                .Replace("minutes", "m")
                .Replace("minute", "m")
                .Replace("min", "m");
        }

        public static string SanitizeHoursFlag(this string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return timeSpan;
            }

            return timeSpan
                .ToLower()
                .Replace("hours", "h")
                .Replace("hour", "h");
        }

        public static string SanitizeDaysFlag(this string timeSpan)
        {
            if (string.IsNullOrEmpty(timeSpan))
            {
                return timeSpan;
            }

            return timeSpan
                .ToLower()
                .Replace("days", "d")
                .Replace("day", "d");
        }
    }
}
