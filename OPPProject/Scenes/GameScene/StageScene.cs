public class StageScene : Scene
{
    public int Width = 40;  // Caution! Map Data 크기와 주의하자.
    public int Height = 40;  // Caution! Map Data 크기와 주의하자.
    public int RenderWidth = 25;
    public int RenderHeight = 15;
    
    private Tile[,] _map;
    private Player _player;
    private List<Enermy> _enermies;
    private TopUI _topUI;
    
    public StageScene(Player p) => Init(p);
    
    public void Init(Player p)
    {
        _map = new Tile[Height, Width];
        _topUI = new TopUI(RenderWidth);
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
                    _map[j, i] = new Tile(new Grass(), new Vector2(j, i));
                else
                    _map[j, i] = new Tile(null, new Vector2(j, i));
            }
        }
        
        // Setting Player
        _player.Map = _map;
        _player.Position = new Vector2(Width / 2, Height / 2);
        _map[_player.Position.Y, _player.Position.X].OnTileObject = _player;
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
        RenderMap();
        _player.Render();
        _topUI.PlayerHpRender(_player.Health.Value);
    }

    public override void Exit()
    {
        _map[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Map = null;
        _player.Health.RemoveListener(_topUI.PlayerHpRender);
    }

    private void PopUpEnermy()
    {
        // 데이터에 맞춰서 진행.
    }

    private void PopUpTreasure()
    {
        // 무야호
    }
    
    private void RenderMap()
    {
        int minRenderWidth = _player.Position.Y - RenderWidth / 2;
        int maxRenderWidth = _player.Position.Y + RenderWidth / 2;
        if (minRenderWidth < 0)
        {
            maxRenderWidth -= minRenderWidth;
            minRenderWidth = 0;
        }
        else if (maxRenderWidth > Width)
        {
            minRenderWidth -= (maxRenderWidth - Width) ;
            maxRenderWidth = Width;
        }
        
        int minRenderHeight = _player.Position.X - RenderHeight / 2;
        int maxRenderHeight = _player.Position.X + RenderHeight / 2;
        if (minRenderHeight < 0)
        {
            maxRenderHeight -= minRenderHeight;
            minRenderHeight = 0;
        }
        else if (maxRenderHeight > Height)
        {
            minRenderHeight -= (maxRenderHeight - Height) ;
            maxRenderHeight = Height;
        }
        
        for (int i = minRenderHeight; i < maxRenderHeight; i++)
        {
            for (int j = minRenderWidth; j < maxRenderWidth; j++)
            {
                _map[j, i].Print();
            }
            Console.WriteLine();
        }
    }
}