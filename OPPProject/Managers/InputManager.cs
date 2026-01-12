using System.Reflection.Metadata.Ecma335;

public static class InputManager
{
    private static ConsoleKey _current;

    private static readonly ConsoleKey[] _keys =
    {
        ConsoleKey.UpArrow, 
        ConsoleKey.DownArrow, 
        ConsoleKey.LeftArrow, 
        ConsoleKey.RightArrow,
        ConsoleKey.Enter,
        ConsoleKey.Z,
        
        // 개발 관련 키들
        ConsoleKey.L, // 로그보기
        ConsoleKey.Q, // 타이틀로 나가기
    };

    public static bool GetKey(ConsoleKey input)
    {
        return _current == input;
    }
    
    // GameManager 에서만 호출 할 예정.
    public static void GetUserInput()
    {
        _current = ConsoleKey.Clear;
        if (Console.KeyAvailable)
        {
            ConsoleKey input = Console.ReadKey().Key;
            if (_keys.Contains(input))
            {
                _current = input;
            }    
        }
    }

    public static void Reset()
    {
        _current = ConsoleKey.Clear;
    }
}