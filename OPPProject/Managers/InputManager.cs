// 사용자 입력 관리 클래스
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

    // 한 Loop 동안 사용자 입력 키에 대한 유지 및 확인.
    public static bool GetKey(ConsoleKey input)
    {
        return _current == input;
    }
    
    // GameManager 에서만 호출 할 예정.
    public static void GetUserInput()
    {
        _current = ConsoleKey.Clear;
        // 키 버퍼 확인. 별다른 입력이 없으면 Interrupt 없이 지나감. 
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