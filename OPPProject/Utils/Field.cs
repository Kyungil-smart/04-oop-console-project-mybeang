public class Field
{
    public Tile[,] _map;   // Hight,Width
    public int Width;
    public int Height;
    
    public Field(int width, int height)
    {
        _map = new Tile[height, width];
        Width = width;
        Height = height;
    }
    
    public Tile this[int x, int y]
    {
        get => _map[x, y];
        set => _map[x, y] = value;
    }

    public GameObject GetObject(int x, int y) => _map[x, y].OnTileObject;
    public GameObject GetObject(Vector2 pos) => _map[pos.X, pos.Y].OnTileObject;

    public void SetObject(GameObject obj)
    {
        _map[obj.Position.X, obj.Position.Y].StepOn(obj);
    }
    public void SetObject(Vector2 pos, GameObject obj)
    {
        _map[pos.X, pos.Y].StepOn(obj);
    }

    public void UnsetObject(GameObject obj)
    {
        _map[obj.Position.X, obj.Position.Y].StepOff();
    }
    
    public void UnsetObject(Vector2 pos)
    {
        _map[pos.X, pos.Y].StepOff();
    }

    public bool IsOutOfMap(Vector2 nxtPos)
        => nxtPos.X >= _map.GetLength(0) || 
           nxtPos.Y >= _map.GetLength(1) || 
           nxtPos.X < 0 || 
           nxtPos.Y < 0;

    public bool IsObstacle(Vector2 nxtPos)
        => (GetObject(nxtPos) is Stone ||
            GetObject(nxtPos) is Tree);
    
    public bool IsNotPlaceable(Vector2 pos)
        => (GetObject(pos.X, pos.Y) is Player ||
            GetObject(pos.X, pos.Y) is Enemy ||
            GetObject(pos.X, pos.Y) is Stone ||
            GetObject(pos.X, pos.Y) is Tree ||
            GetObject(pos.X, pos.Y) is TreasureBox);

    public Vector2 GetRandomPosition()
    {
        int x;
        int y;
        Random rnd = new Random();
        
        while (true)
        {
            x = rnd.Next(1, Height-1);
            y = rnd.Next(1, Width-1);
            if (IsNotPlaceable(new Vector2(x, y))) 
                continue;
            break;
        }
        return new Vector2(x, y);
    }
    
    private static (int min, int max) _calWinSize(int point, int size, int fieldSize)
    {
        int minSize = point - size / 2;
        int maxSize = point + size / 2;
        if (minSize < 0)
        {
            maxSize -= minSize;
            minSize = 0;
        }
        else if (maxSize > fieldSize)
        {
            minSize -= (maxSize - fieldSize) ;
            maxSize = fieldSize;
        }
        return (minSize, maxSize);
    }
    
    public void RenderForPlayer(Player player, RenderWindow rw)
    {
        // 플레이어를 중심으로 필요한 만큼만 Rendering
        (int Min, int Max) width = _calWinSize(player.Position.Y, rw.Width, Width);
        (int Min, int Max) height = _calWinSize(player.Position.X, rw.Height, Height);
        for (int i = height.Min; i < height.Max; i++)
        {
            for (int j = width.Min; j < width.Max; j++)
            {
                _map[i, j].Print();
            }
            Console.WriteLine();
        }
    }
}