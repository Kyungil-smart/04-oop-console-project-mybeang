public enum GameObjectType
{
    Chracter,
    Item,
    Wall,
    Portal,
}

public abstract class GameObject
{
    public char Symbol { get; set; }
    public Vector2 Position { get; set; }
    public Tile[,] Map { get; set; }
    public GameObjectType Type { get; set; }
    
    public abstract void Init();
} 