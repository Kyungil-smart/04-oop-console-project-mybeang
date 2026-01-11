public abstract class GameObject
{
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    public Vector2 Position { get; set; }
    public Field Map { get; set; }
    
    public abstract void Init();
} 