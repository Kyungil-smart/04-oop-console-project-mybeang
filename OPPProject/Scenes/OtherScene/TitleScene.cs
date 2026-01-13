// 타이틀 씬
public class TitleScene : Scene
{
    private MenuList _menus;

    public TitleScene()
    {
        Init();
    }
    
    public void Init()
    {
        _menus = new MenuList();
        _menus.Add("게임 시작", GameStart);
        _menus.Add("크레딧", ViewCredits);
        _menus.Add("게임 종료", GameQuit);
    }
    
    public override void Enter()
    {
        _menus.Reset();
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _menus.CursorUp();
        } 
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _menus.CursorDown();
        }
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _menus.Select();
        }
    }


    public override void Render()
    {
        Console.SetCursorPosition(5, 1);
        GameManager.GameName.Print(ConsoleColor.Yellow);
        _menus.Render(5, 4);
    }

    public override void Exit()
    {
        
    }

    public void GameQuit()
    {
        GameManager.GameOver();
        Console.WriteLine("\n\nGoodbye...\n\n");
    }

    public void GameStart()
    {
        SceneManager.Change(SceneName.Story);
    }

    public void ViewCredits()
    {
        SceneManager.Change(SceneName.Credits);
    }
}