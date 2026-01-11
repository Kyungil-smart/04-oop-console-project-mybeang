public class LogScene : Scene
{
    public override void Enter(){}
    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.L))
        {
            SceneManager.ChangePrevScene();
        }
    }

    public override void Render()
    {
        "< 다시 L키를 누르면 이전 화면으로 돌아갑니다.>\n".Print(ConsoleColor.Cyan);
        Logger.Render();
    }

    public override void Exit()
    {
    }
}