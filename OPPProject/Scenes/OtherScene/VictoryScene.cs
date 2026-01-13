// 승리 씬
public class VictoryScene : Scene
{
    public override void Enter() { }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            SceneManager.Change(SceneName.Title);
        }
    }

    public override void Render()
    {
        Console.SetCursorPosition(5, 5);
        "!!    축하 드립니다     !!".Print(ConsoleColor.Green);
        Console.SetCursorPosition(5, 6);
        "!! 탈출에 성공했습니다 ~ !!".Print(ConsoleColor.Green);
        Console.SetCursorPosition(5, 10);
        "타이틀로 돌아가려면 'Enter Key'를 눌러주세요.".Print();
    }

    public override void Exit() { }
}