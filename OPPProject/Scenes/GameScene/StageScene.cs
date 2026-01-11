public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    private RenderWindow _renderWindow;
    
    private Field _field;
    private Player _player;
    private List<Enermy> _enermies;
    private TreasureBox _treasureBox;
    private TopUI _topUI;
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        _renderWindow = new RenderWindow(25, 15);
        _field = new Field(Height, Width);
        _topUI = new TopUI(_renderWindow.Width);
        _player = p;
        _enermies = new List<Enermy>();
    }
    
    public override void Enter()
    {
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
        _field.SetObject(_player.Position.Y, _player.Position.X, _player);
        _player.Health.AddListener(_topUI.PlayerHpRender);
        
        // Setting Enermy
        List<int> enermyLevels = csvToData.GetEnermy(SceneManager.StageNumber);
        foreach (int level in enermyLevels)
        {
            Enermy e = new Enermy();
            e.SetLevel(level);
            e.Health.AddListener(_topUI.EnermyHpRender);
            _enermies.Add(e);
        }
    }

    public override void Update()
    {
        _player.Update();
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
    }

    public override void Render()
    {
        _field.RenderForPlayer(_player, _renderWindow);
        _player.Render();
        _topUI.PlayerHpRender(_player.Health.Value);
    }

    public override void Exit()
    {
        _field.SetObject(_player.Position.Y, _player.Position.X, null);
        _player.Map = null;
        _player.Health.RemoveListener(_topUI.PlayerHpRender);
    }

    private void PopUpEnermy()
    {
        // 데이터에 맞춰서 진행.
    }

    private void PopUpTreasure()
    {
        int x;
        int y;
        Random rnd = new Random();
        // 랜덤 좌표 
        // PopUp 조건
        // - 해당 좌표가 현재 아래 Object 가 아닐 경우
        while (true)
        {
            x = rnd.Next(0, _field.Height);
            y = rnd.Next(0, _field.Width);
            if (_field[x, y].OnTileObject is Player ||
                _field[x, y].OnTileObject is Enermy ||
                _field[x, y].OnTileObject is Stone ||
                _field[x, y].OnTileObject is Tree) 
                continue;
            break;
        }
        
        // PopUp 시킨 후에 player 에 잊지말고 껴 넣어줘야함.
        
    }
}