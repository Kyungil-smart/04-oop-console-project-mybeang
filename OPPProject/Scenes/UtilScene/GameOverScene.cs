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
            GameManager.GameOver();
        }
        else
        {
            GameManager.GameOver();
        }
    }

    public override void Render()
    {
        Console.SetCursorPosition(5, 5);
        "Game Over".Print(ConsoleColor.Magenta);
        Console.SetCursorPosition(5, 7);
        "Please Pres any key to continue..".Print(ConsoleColor.Gray);
    }
}