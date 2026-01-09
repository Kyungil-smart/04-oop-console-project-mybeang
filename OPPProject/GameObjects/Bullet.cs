/*
 * bullet 에 lifeTime 을 주어, frame 에 맞춘 lifeTime 의 감소로 자체 소멸을 하고 싶었는데,
 * 마땅히 아이디어가 현재 떠오르지 않아 bullet 에 대한 소멸을 player 에서 제어하게 되었음.
*/ 
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