public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    private RenderWindow _renderWindow;
    
    private Field _field;
    private Player _player;
    private List<Enemy> _enemies;
    private List<TreasureBox> _treasureBoxs;
    private TopUI _topUI;
    private StageStatus _stageStatus;
    private int _totalNumOfTb;
    private int _timer;
    private int _popUpEnemyIndex;
    private int _curEnemiesOnMap;
    private int _maxEnemiesOnMap;
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        _stageStatus = StageStatus.None;
        _renderWindow = new RenderWindow(25, 15);
        // _renderWindow = new RenderWindow(Width, Height); // 전체 맵 랜더링; 테스트용
        _field = new Field(Height, Width);
        _topUI = new TopUI(_renderWindow.Width);
        _player = p;
        _enemies = new List<Enemy>();
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
        _player.Map = _field;
        _player.Position = new Vector2(Width / 2, Height / 2);
        _field.SetObject(_player);
        _player.Health.AddListener(_topUI.PlayerHpRender);
        
        // Setting Enemy
        List<int> enemyLevels = csvToData.GetEnemy(SceneManager.StageNumber);
        _maxEnemiesOnMap = enemyLevels[0];
        for (int i = 1; i < enemyLevels.Count - 1; i++)
        {
            Enemy e = new Enemy(_player, i);
            e.SetLevel(enemyLevels[i]);
            e.Map = _field;
            e.IsAlive.AddListener(EnemyIsDead);
            // e.Health.AddListener(_topUI.EnermyHpRender);
            _enemies.Add(e);
        }
        _popUpEnemyIndex = 0;
    }

    public override void Update()
    {
        _player.Update();
        foreach (Enemy e in _enemies)
            e.Update();
        
        if (_player.Health.Value.Current <= 0)
        {
            SceneManager.Change(SceneName.GameOver);
        }

        if (_enemies.Count <= 0 && _stageStatus == StageStatus.Activated)
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
        
        if (_timer % 10 == 9)
            PopUpTreasure();
        
        if (_timer % 20 == 0)
            PopUpEnemy();
        
        _timer++;
    }

    public override void Render()
    {
        _field.RenderForPlayer(_player, _renderWindow);
        _player.Render();
        _topUI.PlayerHpRender(_player.Health.Value);
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
        _treasureBoxs.Clear();
        _enemies.Clear();
        _popUpEnemyIndex = 0;
        _curEnemiesOnMap = 0;
        _maxEnemiesOnMap = 0;
        _stageStatus = StageStatus.Deactivated;
    }

    private void GetAliveEnemyCount()
    {
        
    }

    private void PopUpEnemy()
    {
        int totalEnemies = _enemies.Count - 1;
        if (_curEnemiesOnMap >= _maxEnemiesOnMap) return;
        if (_popUpEnemyIndex >= totalEnemies) return;
        _enemies[_popUpEnemyIndex++].PopUp();
        _curEnemiesOnMap++;
    }

    public void EnemyIsDead(bool alive)
    {
        if (!alive)
            _curEnemiesOnMap--;
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