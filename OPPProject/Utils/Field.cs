public class Field
{
    public Tile[,] _map;
    public int Width;
    public int Height;
    
    public Field(int width, int height)
    {
        _map = new Tile[width, height];
        Width = width;
        Height = height;
    }
    
    public Tile this[int x, int y]
    {
        get => _map[x, y];
        set => _map[x, y] = value;
    }

    public GameObject GetObject(int x, int y) => _map[x, y].OnTileObject;

    public void SetObject(int x, int y, GameObject obj)
    {
        _map[x, y].OnTileObject = obj;
    }

    public bool IsOutOfMap(Vector2 nxtPos)
        => nxtPos.X >= _map.GetLength(0) || 
           nxtPos.Y >= _map.GetLength(1) || 
           nxtPos.X < 0 || 
           nxtPos.Y < 0;

    private (int min, int max) _calcuWinSize(int point, int size)
    {
        int minSize = point - size / 2;
        int maxSize = point + size / 2;
        if (minSize < 0)
        {
            maxSize -= minSize;
            minSize = 0;
        }
        else if (maxSize > Width)
        {
            minSize -= (maxSize - Width) ;
            maxSize = Width;
        }
        return (minSize, maxSize);
    }
    
    public void RenderForPlayer(Player player, RenderWindow rw)
    {
        // 플레이어를 중심으로 필요한 만큼만 Rendering
        (int Min, int Max) width = _calcuWinSize(player.Position.Y, rw.Width);
        (int Min, int Max) height = _calcuWinSize(player.Position.X, rw.Height);
        
        for (int i = height.Min; i < height.Max; i++)
        {
            for (int j = width.Min; j < width.Max; j++)
            {
                _map[j, i].Print();
            }
            Console.WriteLine();
        }
    }
    
    
    public Vector2 FindPath(Vector2 currentPos, Vector2 targetPos)
    {
        Vector2 nxtPos = currentPos;
        
        return nxtPos;
    }
}