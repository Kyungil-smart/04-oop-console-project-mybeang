public class CreditsScene : Scene
{
    private List<(Vector2 pos, string text, ConsoleColor color)> _credits = new();
    private int _width = 25;
    private int _height = 25;
    private int index = 0;
    private Vector2 _topPos = new Vector2(3, 2); 
    
    public override void Enter()
    {
        AddCredit("<< 만든이 >>", ConsoleColor.Blue);
        AddCredit("====================", ConsoleColor.Blue);
        AddCredit("");
        AddCredit("※ 채병희 수강생", ConsoleColor.Yellow);
        AddCredit("");
        AddCredit("<< 감사한 분들 >>", ConsoleColor.Blue);
        AddCredit("====================", ConsoleColor.Blue);
        AddCredit("");
        AddCredit("★ 김재성 강사님", ConsoleColor.Green);
        AddCredit("★ 최영민 강사님", ConsoleColor.Green);
        AddCredit("★ 이태호 학습매니저님", ConsoleColor.Green);
    }

    private void AddCredit(string text, ConsoleColor color = ConsoleColor.Gray)
    {
        Vector2 headerPos = new Vector2(_height, 2);
        _credits.Add((headerPos + Vector2.Down * index, text, color));
        index++;
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
            for (int j = 0; j < _credits.Count; j++)
            {
                Logger.Debug($"i={i} {_credits[j].pos.ToString()} {_credits[j].text}");
                Console.SetCursorPosition(2, _credits[j].pos.X);
                _credits[j].text.Print(_credits[j].color);
                _credits[j] = (_credits[j].pos + Vector2.Up, _credits[j].text, _credits[j].color);
            }
            Thread.Sleep(100);
        } 
        Console.SetCursorPosition(2, _height);
        "엔터 입력시 타이틀로 돌아갑니다.".Print();
    }
}