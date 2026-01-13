// 맵 관리용 클래스
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

    // 맵 바깥으로 나가는지 확인
    public bool IsOutOfMap(Vector2 nxtPos)
        => nxtPos.X >= _map.GetLength(0) || 
           nxtPos.Y >= _map.GetLength(1) || 
           nxtPos.X < 0 || 
           nxtPos.Y < 0;

    // 충돌물 감지
    public bool IsObstacle(Vector2 nxtPos) => GetObject(nxtPos) is IObstacle;
    
    // GameObject가 겹치지 않도록 감지
    public bool IsNotPlaceable(Vector2 pos) => GetObject(pos) is INotPlaceable;

    // 랜덤 좌표 획득
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
    
    // 맵에서 플레이어를 중심으로 필요 만큼만 Rendering
    public void RenderForPlayer(Player player, RenderWindow rw)
    {
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