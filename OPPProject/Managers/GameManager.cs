public class GameManager
{
    public static bool IsGameOver { get; private set; }
    public const string GameName = "Explore Console World";
    
    private PlayerCharactor _player;
    
    public void Run()
    {
        Init();
        Logger.Info("Game Started");
        while (!IsGameOver)
        {
            Console.Clear();
            SceneManager.Render();
            InputManager.GetUserInput();

            if (InputManager.GetKey(ConsoleKey.L))
            {
                SceneManager.Change(SceneName.Log);
            }
            
            SceneManager.Update();
        }
    }

    private void Init()
    {
        IsGameOver = false;
        _player = new PlayerCharactor();
        
        SceneManager.Add(SceneName.Title, new TitleScene());
        SceneManager.Add(SceneName.Story, new StoryScene());
        SceneManager.Add(SceneName.Stage, new StageScene(_player));
        SceneManager.Add(SceneName.Credits, new CreditsScene());
        SceneManager.Add(SceneName.GameOver, new GameOverScene());
        SceneManager.Add(SceneName.Log, new LogScene());

        SceneManager.OnChangeScene += InputManager.Reset;
        SceneManager.Change(SceneName.Title);
    }
    public static void GameOver()
    {
        IsGameOver = true;
    }
}