public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    private RenderWindow _renderWindow;
    
    private Field _field;
    private Player _player;
    private EnemyManager _emanager;
    private List<TreasureBox> _treasureBoxs;
    private TopUI _topUI;
    private StageStatus _stageStatus;
    private int _totalNumOfTb;
    private int _timer;
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        _stageStatus = StageStatus.None;
        _renderWindow = new RenderWindow(25, 15);
        // _renderWindow = new RenderWindow(Width, Height); // 전체 맵 랜더링; 테스트용
        _field = new Field(Height, Width);
        _topUI = new TopUI(_renderWindow.Width);
        _player = p;
        _emanager = new();
        _treasureBoxs = new List<TreasureBox>();
        _totalNumOfTb = csvToData.GetNumOfTreasuerBox(SceneManager.StageNumber);
    }
    
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
                    _field[j, i] = new Tile(new Grass(), new Vector2(j, i));
                else
                    _field[j, i] = new Tile(null, new Vector2(j, i));
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
        if (_timer % 10 == 9)
            PopUpTreasure();
        
        if (_timer % 20 == 0)
            _emanager.PopUpEnemy();
        
        _player.Update();
        _emanager.Update();
        
        if (_player.Health.Value.Current <= 0)
        {
            SceneManager.Change(SceneName.GameOver);
        }

        if (_emanager.IsAllKilled() && _stageStatus == StageStatus.Activated)
        {
            SceneManager.Change(SceneName.Victory);
            // if (SceneManager.StageNumber < 5)
            // {
            //     // 다음 스테이지 간다는 문구 보이기 (ractangle 활용하기)
            //     // Global Timer 생기면 일정 시간만 보이게....
            //     SceneManager.StageNumber++;
            // }
            // else
            // {
            //     SceneManager.Change(SceneName.Victory);
            // }
        }
        _timer++;
    }

    public override void Render()
    {
        _field.RenderForPlayer(_player, _renderWindow);
        _player.Render();
        _topUI.PlayerHpRender(_player.Health.Value);
        _topUI.RemainEnemy(_emanager.EnemyState.Value);
        if (_treasureBoxs.Count > 0)
        {
            foreach (TreasureBox treasureBox in _treasureBoxs)
            {
                if (treasureBox.IsOpenedBox)
                    treasureBox.Render();
            }
        }
    }

    public override void Exit()
    {
        _field.SetObject(_player.Position, null);
        _player.Map = null;
        _player.Health.RemoveListener(_topUI.PlayerHpRender);
        _emanager.EnemyState.RemoveListener(_topUI.RemainEnemy);
        _treasureBoxs.Clear();
        _emanager.Clear();
        _stageStatus = StageStatus.Deactivated;
    }

    private void PopUpTreasure()
    {
        if (_treasureBoxs.Count < _totalNumOfTb)
        {
            TreasureBox treasureBox = new TreasureBox(_field.GetRandomPosition(), _player);
            _treasureBoxs.Add(treasureBox);    
        }
    }
}