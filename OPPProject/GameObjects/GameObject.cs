public abstract class GameObject
{
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    public Vector2 Position { get; set; }
    public Tile[,] Map { get; set; }
    public GameObjectType Type { get; set; }
    
    public abstract void Init();

    public static bool IsOutOfMap(Tile[,] map, Vector2 nxtPos)
    {
        return nxtPos.X >= map.GetLength(0) || 
               nxtPos.Y >= map.GetLength(1) || 
               nxtPos.X < 0 || 
               nxtPos.Y < 0;
    }  
} 