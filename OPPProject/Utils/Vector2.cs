public struct Vector2
{
    public int X;
    public int Y;
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public static Vector2 Up => new Vector2(-1, 0);
    public static Vector2 Down => new Vector2(1, 0);
    public static Vector2 Left => new Vector2(0, -1);
    public static Vector2 Right => new Vector2(0, 1);

    public static Vector2 operator +(Vector2 a, Vector2 b)
        => new Vector2(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator *(Vector2 a, int b)
    {
        return new Vector2(a.X * b, a.Y * b);
    }

    public string ToString()
    {
        return $"[{X}, {Y}]";
    }
}