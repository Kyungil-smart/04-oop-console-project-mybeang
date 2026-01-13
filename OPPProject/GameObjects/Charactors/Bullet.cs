/*
 * bullet 에 lifeTime 을 주어, frame 에 맞춘 lifeTime 의 감소로 자체 소멸을 하고 싶었는데,
 * 마땅히 아이디어가 현재 떠오르지 않아 bullet 에 대한 소멸을 player 에서 제어하게 되었음.
*/ 
public class Bullet : GameObject
{
    public int Damage;
    public int Range;
    private Direction _direction;

    public Bullet(Player player, Vector2 position)
    {
        _direction = player.Direction;
        Damage = player.Damage;
        Range = player.Range;
        Position = position;
        Map = player.Map;
        Init();
    }
    
    public override void Init()
    {
        Symbol = '*';
        Color = ConsoleColor.Yellow;
        Map.SetObject(this);
    }
    
    public void Move()
    {
        Vector2 nxtPos = Position; 
        // Player 의 현 이동 방향을 이용하여 진행.
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
        // 총알이 바깥에 나가지 않게
        if (Map.IsOutOfMap(nxtPos))
        {
            Range = 0;
            return;
        }
        
        GameObject nextTileObject = Map.GetObject(nxtPos);
        if (nextTileObject != null && nextTileObject is ICrashBullet)
            (nextTileObject as ICrashBullet).CrashBullet(this);
        
        Map.UnsetObject(this);
        Map.SetObject(nxtPos, this);
        Position = nxtPos;
        Range--;
    }

    public void Remove()
    {
        Range = 0;
        Map.UnsetObject(this);
    }
}