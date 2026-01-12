public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    private RenderWindow _renderWindow;
    
    private Field _field;
    private Player _player;
    private List<Enermy> _enermies;
    private List<TreasureBox> _treasureBoxs;
    private TopUI _topUI;
    private int _totalNumOfTb;
    private int _timer;
    private int _popUpEnermyIndex;
    private int _curEnermiesOnMap;
    private int _maxEnermiesOnMap;
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        // _renderWindow = new RenderWindow(25, 15);
        _renderWindow = new RenderWindow(40, 40);
        _field = new Field(Height, Width);
        _topUI = new TopUI(_renderWindow.Width);
        _player = p;
        _enermies = new List<Enermy>();
        _treasureBoxs = new List<TreasureBox>();
        _totalNumOfTb = csvToData.GetNumOfTreasuerBox(SceneManager.StageNumber);
    }
    
    public override void Enter()
    {
        _timer = 0;
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
        
        // Setting Enermy
        List<int> enermyLevels = csvToData.GetEnermy(SceneManager.StageNumber);
        _maxEnermiesOnMap = enermyLevels[0];
        for (int i = 1; i < enermyLevels.Count - 1; i++)
        {
            Enermy e = new Enermy(_player, i);
            e.SetLevel(enermyLevels[i]);
            e.Map = _field;
            e.IsAlive.AddListener(EnermyIsDead);
            // e.Health.AddListener(_topUI.EnermyHpRender);
            _enermies.Add(e);
        }
        _popUpEnermyIndex = 0;
    }

    public override void Update()
    {
        _player.Update();
        foreach (Enermy e in _enermies)
            e.Update();
        
        if (_player.Health.Value.Current <= 0)
        {
            SceneManager.Change(SceneName.GameOver);
        }

        if (_enermies.Count <= 0)
        {
            if (SceneManager.StageNumber < 5)
            {
                // 다음 스테이지 간다는 문구 보이기 (ractangle 활용하기)
                // Global Timer 생기면 일정 시간만 보이게....
                SceneManager.StageNumber++;
            }
            else
            {
                SceneManager.Change(SceneName.Victory);
            }
        }
        
        if (_timer % 10 == 0) // FIXME: keyabilable 추가시 적절히 변경해야함.
        {
            PopUpTreasure();
            PopUpEnermy();
        }
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
        _enermies.Clear();
        _popUpEnermyIndex = 0;
        _curEnermiesOnMap = 0;
        _maxEnermiesOnMap = 0;
    }

    private void PopUpEnermy()
    {
        int totalEnermies = _enermies.Count - 1;
        if (_curEnermiesOnMap >= _maxEnermiesOnMap) return;
        if (_popUpEnermyIndex >= totalEnermies) return;
        _enermies[_popUpEnermyIndex++].PopUp();
        _curEnermiesOnMap++;
    }

    public void EnermyIsDead(bool alive)
    {
        if (!alive)
            _curEnermiesOnMap--;
        Logger.Debug($"Remain Enermy cnt {_curEnermiesOnMap}");
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