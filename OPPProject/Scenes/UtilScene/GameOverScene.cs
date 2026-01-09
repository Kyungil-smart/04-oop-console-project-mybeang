public class GameOverScene : Scene
{
    public override void Enter()
    {
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
        Console.SetCursorPosition(5, 5);
        "Game Over".Print(ConsoleColor.Magenta);
        Console.SetCursorPosition(5, 7);
        "Please Pres Enter key to continue..".Print(ConsoleColor.Gray);
    }
}