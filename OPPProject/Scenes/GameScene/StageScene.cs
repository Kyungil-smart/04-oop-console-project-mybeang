// 게임 진행 씬
public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    private RenderWindow _renderWindow;
    
    private Field _field;  // 맵
    private Player _player;  // 플레이어
    private EnemyManager _emanager;  // 적 관리 객체
    private List<BuffBox> _buffBoxs;  // 버프 박스 관리
    private TopUI _topUI;  // 화면 맨 위 UI 용
    private StageStatus _stageStatus;  // 현 스태이지 상태
    private int _totalNumOfBb;  // 총 버프 박스 개수 
    private int _timer;  // 게임 진행 내 Timer
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        // 스테이지 생성시 필요한 데이터 정리
        _stageStatus = StageStatus.None;
        _renderWindow = new RenderWindow(25, 15);
        // _renderWindow = new RenderWindow(Width, Height); // 전체 맵 랜더링; 테스트용
        _field = new Field(Height, Width);
        _topUI = new TopUI(_renderWindow.Width);
        _player = p;
        _emanager = new();
        _buffBoxs = new List<BuffBox>();
        _totalNumOfBb = csvToData.GetNumOfBuffBox(SceneManager.StageNumber);
    }
    
    // 스테이지 씬 진입시 필요한 데이터 정리
    public override void Enter()
    {
        _timer = 0;
        _stageStatus = StageStatus.Activated;
        // Setting Map
        string[,] mapData = csvToData.GetMap(SceneManager.StageNumber);
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (mapData[i, j] == "G")
                    _field[i, j] = new Tile(new Grass(), new Vector2(i, j));
                else if (mapData[i, j] == "T")
                    _field[i, j] = new Tile(new Tree(), new Vector2(i, j));
                else if (mapData[i, j] == "S")
                    _field[i, j] = new Tile(new Stone(), new Vector2(i, j));
                else
                    _field[i, j] = new Tile(null, new Vector2(i, j));
            }
        }
        
        // Setting Player
        if (SceneManager.StageNumber == 1)
            _player.Health.Value = new Hp(1, 1);
        _player.Map = _field;
        _player.Position = new Vector2(Width / 2, Height / 2);
        _field.SetObject(_player);
        _player.Health.AddListener(_topUI.PlayerHpRender);
        
        // Setting Enemy
        _emanager.Setting(_player);
        _emanager.EnemyState.AddListener(_topUI.RemainEnemy);
    }

    public override void Update()
    {
        if (_timer % 20 == 19)
            // 버프 박스 팝업 
            // 첫번째 박스는 19초 후
            // 이후 20초마다 1개씩 필드위 발생 
            PopUpBuffBox();
        
        if (_timer % 20 == 0)
            // 적 개체 팝업
            // 첫번째 적은 0초 후 
            // 이후 20초마다 1마리씩 필드위 발생
            _emanager.PopUpEnemy();
        
        _player.Update();
        _emanager.Update();
        
        // 플레이어 체력이 0이하일 경우 게임 오버
        if (_player.Health.Value.Current <= 0)
            SceneManager.Change(SceneName.GameOver);
        

        if (_emanager.IsAllKilled() && _stageStatus == StageStatus.Activated)
        {
            // 모든 적을 격퇴하고 스테이지가 Activated 상태일 경우 다음 스테이지 이동
            if (SceneManager.StageNumber < 5)
                GoToNextLevel();
            else
                // 모든 스테이지 완료시 Victory
                SceneManager.Change(SceneName.Victory);
        }
        _timer++;
    }

    // 맵내 기본 랜더링 진행
    public override void Render()
    {
        _field.RenderForPlayer(_player, _renderWindow);
        _player.Render();
        _topUI.PlayerHpRender(_player.Health.Value);
        _topUI.RemainEnemy(_emanager.EnemyState.Value);
        _topUI.ViewCurrentStage(TopUiAlign.Center, _renderWindow.Width / 2, 0);
        if (_buffBoxs.Count > 0)
        {
            foreach (BuffBox buffBox in _buffBoxs)
            {
                if (buffBox.IsOpenedBox)
                    buffBox.Render();
            }
        }
    }

    // 스테이지 탈출시 데이터 초기화
    public override void Exit()
    {
        _field.SetObject(_player.Position, null);
        _player.Map = null;
        _player.Health.RemoveListener(_topUI.PlayerHpRender);
        _emanager.EnemyState.RemoveListener(_topUI.RemainEnemy);
        _buffBoxs.Clear();
        _emanager.Clear();
        _stageStatus = StageStatus.Deactivated;
    }

    // 버프 박스 팝업
    private void PopUpBuffBox()
    {
        // 스테이지별 총 팝업 개수 만큼만 팝업
        if (_buffBoxs.Count < _totalNumOfBb)
        {
            BuffBox buffBox = new BuffBox(_field.GetRandomPosition(), _player);
            _buffBoxs.Add(buffBox);    
        }
    }
    
    private void GoToNextLevel()
    {
        GameManager.IsPaused = true;
        string nxText = "Go to Next Level >>";
        Ractangle r = new Ractangle(2, 3, nxText.Length + 2,3);
        r.Draw();
        Console.SetCursorPosition(3, 4);
        nxText.Print(ConsoleColor.Magenta);
        Thread.Sleep(3000); // wait 3 secs
        GameManager.IsPaused = false;
        SceneManager.StageNumber++;
        SceneManager.Change(SceneName.Void);
    }
}