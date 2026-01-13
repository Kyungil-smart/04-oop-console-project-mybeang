public enum LogType
{
    Info,
    Warning,
    Debug
}

// Log 찍기위한 클래스
public static class Logger
{
    private static List<(LogType type, string text)> _logList = new();

    public static void Info(string msg)
    {
        _logList.Add((LogType.Info, msg));
    }
    public static void Warning(string msg)
    {
        _logList.Add((LogType.Warning, msg));
    }
    public static void Debug(string msg)
    {
        _logList.Add((LogType.Debug, msg));
    }

    public static void Render()
    {
        foreach ((LogType type, string text) in _logList)
        {
            if (type == LogType.Debug) text.Print(ConsoleColor.DarkGreen);
            else if (type == LogType.Warning) text.Print(ConsoleColor.Yellow);
            else text.Print();
            Console.WriteLine();
        }
    }
}