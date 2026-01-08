

public class Charactor : GameObject
{
    public Charactor() => Init();
    public override void Init()
    {
        Symbol = 'C';
    }

    private void Move(Vector2 direction)
    {
        Vector2 nextPos = Position + direction;

        if (nextPos.X >= Map.GetLength(0) || nextPos.Y >= Map.GetLength(1) ||
            nextPos.X < 0 || nextPos.Y < 0)
        {
            return;
        }
        Map[Position.Y, Position.X].OnTileObject = null;
        Map[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;
    }

    public virtual void Update(){}
}