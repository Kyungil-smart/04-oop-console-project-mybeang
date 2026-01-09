public class Bullet : GameObject
{
    private Player _player;
    public int Damage;
    public int Range;
    private Direction _direction;

    public Bullet(Player player, Vector2 position, int damage, int range, Direction direction)
    {
        _player = player;
        _direction = direction;
        
        Damage = damage;
        Range = range;
        Position = position;
        Map = player.Map;
        Init();
    }
    
    public override void Init()
    {
        Symbol = '*';
        Color = ConsoleColor.Yellow;
        Type = GameObjectType.Chracter;
        Map[Position.Y, Position.X].StepOn(this);
    }
    
    public void Move()
    {
        Vector2 nxtPos = Position; 
        switch (_direction)
        {
            case Direction.Up:
                nxtPos += Vector2.Up;
                break;
            case Direction.Down:
                nxtPos += Vector2.Down;
                break;
            case Direction.Left:
                nxtPos += Vector2.Left;
                break;
            default:  // Right
                nxtPos += Vector2.Right;
                break;
        }
        if (IsOutOfMap(Map, nxtPos))
            return;
        
        Map[Position.Y, Position.X].StepOff();
        Map[nxtPos.Y, nxtPos.X].StepOn(this);
        Position = nxtPos;
        Range--;
    }

    public void Disappear()
    {
        Map[Position.Y, Position.X].StepOff();
    }
}