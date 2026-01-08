public class CreditsScene : Scene
{
    private List<(Vector2 pos, string text, ConsoleColor color)> _thanks = new();
    private int _width = 25;
    private int _height = 25;
    private Vector2 _topPos = new Vector2(3, 2); 
    
    public override void Enter()
    {
        Vector2 headerPos = new Vector2(_height, 2);
        // Header
        _thanks.Add((headerPos, "<< 감사한 분들 >>", ConsoleColor.Blue));
        _thanks.Add((headerPos + Vector2.Down, "====================", ConsoleColor.Blue));
        
        _thanks.Add((headerPos + Vector2.Down * 3, "★ 김재성 강사님", ConsoleColor.Green));
        _thanks.Add((headerPos + Vector2.Down * 4, "★ 최영민 강사님", ConsoleColor.Green));
        _thanks.Add((headerPos + Vector2.Down * 5, "★ 이태호 학습매니저님", ConsoleColor.Green));
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            SceneManager.Change(SceneName.Title);
        }
    }

    public override void Render()
    {
        for (int i = _height + 1; i >= _topPos.X; i--)
        {
            Console.Clear();
            for (int j = 0; j < _thanks.Count; j++)
            {
                Logger.Debug($"i={i} {_thanks[j].pos.ToString()} {_thanks[j].text}");
                Console.SetCursorPosition(2, _thanks[j].pos.X);
                _thanks[j].text.Print(_thanks[j].color);
                _thanks[j] = (_thanks[j].pos + Vector2.Up, _thanks[j].text, _thanks[j].color);
            }
            Thread.Sleep(100);
        } 
        Console.SetCursorPosition(2, _height);
        "엔터 입력시 타이틀로 돌아갑니다.".Print();
    }
}