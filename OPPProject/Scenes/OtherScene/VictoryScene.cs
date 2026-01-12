public class VictoryScene : Scene
{
    public override void Enter()
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
        Console.SetCursorPosition(5, 5);
        "Victory".Print(ConsoleColor.Green);
        Console.SetCursorPosition(5, 7);
        "Please Pres Enter key to continue..".Print(ConsoleColor.Gray);
    }

    public override void Exit()
    {
        
    }
}