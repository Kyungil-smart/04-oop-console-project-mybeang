public class GameManager
{
    public static bool IsGameOver { get; private set; }
    public const string GameName = "<< Survive Console World >>";  // 나름 심사숙고해서 결정한 게임 이름 입니다?
    public static bool IsPaused { get; set; }
    
    private Player _player;
    
    public void Run()
    {
        Init();
        // 메인 루프
        while (!IsGameOver)
        {
            Console.Clear();
            SceneManager.Render();
            InputManager.GetUserInput();
            
            // 개발용 Logic. 데이터 확인을 위한 로그를 찍고 확인을 위함.
            if (InputManager.GetKey(ConsoleKey.L))
            {
                SceneManager.Change(SceneName.Log);
            }
            
            SceneManager.Update();
            Thread.Sleep(100);  // 10 FPS
        }
    }

    // 게임 시작시 필요 내용 정리.
    private void Init()
    {
        IsPaused = false;
        IsGameOver = false;
        _player = new Player();
        
        SceneManager.StageNumber = 1;
        SceneManager.Add(SceneName.Title, new TitleScene());
        SceneManager.Add(SceneName.Story, new StoryScene());
        SceneManager.Add(SceneName.Stage, new StageScene(_player));
        SceneManager.Add(SceneName.Credits, new CreditsScene());
        SceneManager.Add(SceneName.GameOver, new GameOverScene());
        SceneManager.Add(SceneName.Victory, new VictoryScene());
        SceneManager.Add(SceneName.Log, new LogScene());
        SceneManager.Add(SceneName.Void, new VoidScene());
        
        SceneManager.OnChangeScene += InputManager.Reset;
        SceneManager.Change(SceneName.Title);
    }
    public static void GameOver()
    {
        IsGameOver = true;
    }
}