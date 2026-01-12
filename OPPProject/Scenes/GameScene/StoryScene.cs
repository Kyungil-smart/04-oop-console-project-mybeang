public class StoryScene : Scene
{
    private List<string> _storyList = new();
    private List<(string text, ConsoleColor color)> _howToPlayList = new();
    private List<string> _tailList = new();

    public override void Enter()
    {
        // JSON 등이 좋지만 일단 Code에 직접 작성
        _storyList.Add("총 5개의 스테이지를 클리어하여 이 콘솔 세계에서 탈출하세요!");
        _storyList.Add("적을 향해 공격하여 적을 쓰러뜨리세요!");
        _storyList.Add("나무나 바위에는 지나갈 수 없으니 주의하세요!");
        
        _howToPlayList.Add(("조작법",  ConsoleColor.Cyan));
        _howToPlayList.Add(("--------------------------",  ConsoleColor.Cyan));
        _howToPlayList.Add(("- 이동: 방향키(↑, →, ←, ↓)", ConsoleColor.Gray));
        _howToPlayList.Add(("- 공격: Z 키", ConsoleColor.Gray));
        _howToPlayList.Add(("- 게임 아이콘", ConsoleColor.Gray));
        _howToPlayList.Add(("  > 플레이어: ▲",  ConsoleColor.Blue));
        _howToPlayList.Add(("  > 총알: * (플레이어의 삼각형 꼭지점 방향으로 총알이 나갑니다)", ConsoleColor.Yellow));
        _howToPlayList.Add(("  > 버프: B", ConsoleColor.Cyan));
        _howToPlayList.Add(("  > 적: E", ConsoleColor.Red));
        _howToPlayList.Add(("  > 바위: S", ConsoleColor.Gray));
        _howToPlayList.Add(("  > 나무: T", ConsoleColor.DarkGreen));
        
        _tailList.Add("엔터키 입력 시 게임이 시작됩니다!");
        _tailList.Add("즐거운 게임 되세요~!");
    }

    public override void Update()
    {
        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                SceneManager.Change(SceneName.Stage);        
                break;
            }
        }
    }

    public override void Render()
    {
        int y = 4;
        // 간단한 스토리 표기.
        Console.SetCursorPosition(2, 2);
        GameManager.GameName.Print(ConsoleColor.Yellow);
        foreach (string story in _storyList)
        {
            Console.SetCursorPosition(2, y++);
            story.Print();
        }
        y++;
        // 조작법 표기.
        foreach ((string text, ConsoleColor color) howToPlay in _howToPlayList)
        {
            Console.SetCursorPosition(2, y++);
            howToPlay.text.Print(howToPlay.color);
        }
        y += 5;
        foreach (string tail in _tailList)
        {
            Console.SetCursorPosition(2, y++);
            tail.Print();
        }
    }

    public override void Exit()
    {
        _storyList.Clear();
        _howToPlayList.Clear();
        _tailList.Clear();
    }
    
}