namespace TimeSpanStringsCalculator.Models
{
    public class InvalidTimeSpanFormatException : Exception
    {
        public InvalidTimeSpanFormatException(string invalidLog) : base($"The log \"{invalidLog}\" is not in the correct format.")
        {
        }
    }
}
