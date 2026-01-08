public class StoryScene : Scene
{
    public override void Enter()
    {
        Console.WriteLine("Entering Story");
    }

    public override void Update()
    {
        
    }

    public override void Render()
    {
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            Console.WriteLine("Entering Story");
        }
    }

    public override void Exit()
    {
        Console.WriteLine("Exit Story");
    }
    
}