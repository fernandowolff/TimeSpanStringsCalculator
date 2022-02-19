namespace TimeSpanStringsCalculator 
{
    public class Program {
        public const string ProgramTitle = "Time Span Strings Calculator";
        public const string TimeSpansLabel = "Time spans (comma separated): ";
        public const string TimeSpansErrorMessage = "Please enter valid time spans separated by commas.";
        public const string NextIterationInstruction = "Hit Esc to exit the program or any other key for a new convertion.";
        public const string GoodbyeMessage = "See yah!!";

        static void Main(string[] args)
        {
            var exit = false;
            var converter = new Converter();

            Console.WriteLine(ProgramTitle);

            BreakLine();

            do
            {
                ReadTimeSpans(out string entry);

                if (string.IsNullOrEmpty(entry))
                {
                    EnterOnErrorCondition();
                }
                else
                {
                    try
                    {
                        var sanitizedEntry = entry.Replace(" ", string.Empty);
                        var timeSpans = sanitizedEntry.Split(',');

                        var formattedTimeSpan = converter.GetTimeSpansSumFormatted(timeSpans);
                        var timeSpanInDays = converter.SumTimeSpansToDays(timeSpans);
                        var timeSpanInHours = converter.SumTimeSpansToHours(timeSpans);
                        var timeSpanInMinutes = converter.SumTimeSpansToMinutes(timeSpans);
                        var timeSpanInSeconds = converter.SumTimeSpansToSeconds(timeSpans);

                        BreakLine();

                        Console.WriteLine($"Results");

                        Console.WriteLine($"Formatted: {formattedTimeSpan}");
                        Console.WriteLine($"Days: {timeSpanInDays.ToString("0.0000")}");
                        Console.WriteLine($"Hours: {timeSpanInHours.ToString("0.0000")}");
                        Console.WriteLine($"Minutes: {timeSpanInMinutes.ToString("0.0000")}");
                        Console.WriteLine($"Seconds: {timeSpanInSeconds.ToString("0.0000")}");

                        BreakLine();

                        Console.WriteLine(new String('=', Console.WindowWidth));

                        BreakLine();

                        Console.WriteLine(NextIterationInstruction);

                        var key = Console.ReadKey();

                        if(key.Key == ConsoleKey.Escape)
                        {
                            exit = true;
                        }
                        else
                        {
                            PrepareForAnotherReading();
                        }
                    }
                    catch
                    {
                        EnterOnErrorCondition();
                    }
                }
            } while (!exit);

            FinalizeProgram();
        }

        private static void BreakLine()
        {
            Console.WriteLine("");
        }

        private static string ReadTimeSpans(out string entry)
        {
            Console.Write(TimeSpansLabel);

            entry = Console.ReadLine();

            return entry;
        }

        private static void EnterOnErrorCondition()
        {
            DisplayErrorMessage();

            Thread.Sleep(3000);

            ClearError();
        }

        private static void DisplayErrorMessage()
        {
            Console.Beep();
            BreakLine();
            Console.WriteLine(TimeSpansErrorMessage);
        }

        private static void ClearError()
        {
            ClearLine();

            Console.SetCursorPosition(0, Console.CursorTop - 1);

            ClearLine();

            Console.SetCursorPosition(0, Console.CursorTop - 1);

            ClearLine();

            Console.SetCursorPosition(0, Console.CursorTop - 1);

            ClearLine();
        }

        private static void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void PrepareForAnotherReading()
        {
            ClearLine();

            Console.SetCursorPosition(0, Console.CursorTop - 1);

            ClearLine();
        }

        private static void FinalizeProgram()
        {
            Console.Clear();

            Console.Write("s"); //wtf
            Console.WriteLine(GoodbyeMessage);

            Thread.Sleep(500);

            Environment.Exit(0);
        }
    }
}