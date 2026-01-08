public class StageScene : Scene
{
    private int _width = 20;
    private int _height = 20;
    private Tile[,] _map;
    private PlayerCharactor _player;
    
    public StageScene(PlayerCharactor p) => Init(p);
    
    public void Init(PlayerCharactor p)
    {
        _map = new Tile[_height, _width];
        _player = p;
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _map[j, i] = new Tile(null, new Vector2(j, i));
            }
        }
    }
    public override void Enter()
    {
        _player.Map = _map;
        _player.Position = new Vector2(1, 1);
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
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _map[j, i].Print();
            }
            Console.WriteLine();
        }
    }
}