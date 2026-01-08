public class StageScene : Scene
{
    public int Width = 40;
    public int Height = 40;
    public int RenderWidth = 20;
    public int RenderHeight = 20;
    
    private Tile[,] _map;
    private PlayerCharactor _player;
    
    public StageScene(PlayerCharactor p) => Init(p);
    
    public void Init(PlayerCharactor p)
    {
        _map = new Tile[Height, Width];
        _player = p;
        // Real Map Init
        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                _map[j, i] = new Tile(null, new Vector2(j, i));
            }
        }
    }
    public override void Enter()
    {
        _player.Map = _map;
        _player.Position = new Vector2(Width / 2, Height / 2);
        _map[_player.Position.Y, _player.Position.X].OnTileObject = _player;
    }

    public override void Update()
    {
        _player.Update();
    }

    public override void Render()
    {
        RenderMap();
        _player.Render();
    }

    public override void Exit()
    {
        _map[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Map = null;
    }
    
    private void RenderMap()
    {
        for (int i = 0; i < RenderHeight; i++)
        {
            for (int j = 0; j < RenderWidth; j++)
            {
                _map[j, i].Print();
            }
            Console.WriteLine();
        }
    }
}