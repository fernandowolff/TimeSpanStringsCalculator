# Time Span Strings Calculator

This is a C# Console App for calculating string time spans in different output formats.

The intention is to facilitate my life when I get lost on my work time logs... when I finish loging my time and see that some time is missing. After a day full of different tasks it gets harder to find where I made a mistake. This console app will help.

# How to use

The program asks you to enter with the time spans to calculate separated by commas:

```
Time spans (comma separated): 15m,15m,10m,55min,1h25m,3h40min,5min
```

After entering with the times to be calculated, you hit ENTER. The program will sum the times and display them on different formats:
```
Results
Formatted: 6h45m
Days: 0.2813
Hours: 6.7500
Minutes: 405.0000
Seconds: 24300.0000
```

After displaying the results, the program will present a message like:
```
Hit Esc to exit the program or any other key for a new convertion.
```

Just as it tells, you can hit ESC for finishing the program. Or then you can hit any other key in order to make another read. In that case, the program simply asks you for another entry of time spans.

## Time Span Formats

The time spans are made of 4 parts: days, hours, minutes and seconds.

|Part|Possible Flags|
|-|-|
|Days|days, day or d|
|Hours|hours, hour, h|
|Minutes|minutes, minute, min, m|
|Seconds|seconds, second, sec, s|

The white spaces around the time spans do not matter. All white spaces are trimmed before validation and calculation.

# TODOS

- Functionality to configure number of decimal places on the results.
- Different colors for different sections of the program.
- Functionality to save the logs in a json file.
- Functionality to enter with single time spans, while it keeps displaying the current session/day logs in a table.
  - Functionality to associate a time range to the time span, like: "1h, 8:00 - 9:00".
  - Functionality to associate a description to the time span, like: "1h, Task 12344 - cleaning up code".
- Functionality to add a Pomodoro timer 25/5.
  - Functionality to customize the Pomodoro configurations. 
