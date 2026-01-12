public class Enermy : Charactor, IInteractable, ICrashBullet
{
    private int _level;
    private Player _player;
    private int _id;
    public ObservableProperty<bool> IsAlive = new();
    
    public Enermy(Player player, int id)
    {
        _id = id;
        _player = player;
        IsAlive.Value = false;
        Init();
    }

    public void Init()
    {
        Symbol = 'E';
        Color = ConsoleColor.Red;
    }

    public void SetLevel(int level)
    {
        _level = level;  
        Health.Value = new Hp(2 * _level, 2 * _level);
    } 
    
    public void TakeDamage(int damage)
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - damage };
        if (Health.Value.Current <= 0)
            Dead();
    }
    
    public void Interact(Player player)
    {
        // 플레이어가 데미지를 입는다.
        player.TakeDamage();
    }

    public void CrashBullet(Bullet bullet)
    {
        // 몬스터가 데미지를 입는다.
        TakeDamage(bullet.Damage);
        bullet.Remove();
        Logger.Info($"{_id}: curHp={Health.Value.Current}");
    }

    public override void Update()
    {
        if (IsAlive.Value)
            _autoMove();
    }
    
    protected override void Move(Vector2 nxtPos)
    {
        if (Map.IsOutOfMap(nxtPos)) return;
        if (nxtPos == _player.Position) return;
        if (GameManager.IsPaused) return;
        Map.UnsetObject(this);
        Map.SetObject(nxtPos, this);
        Position = nxtPos;
    }

    public void Render() {}

    public void PopUp()
    {
        Position = Map.GetRandomPosition();
        Map.SetObject(this);
        IsAlive.Value = true;
    }

    public void Dead()
    {
        IsAlive.Value = false;
        Map.UnsetObject(this);
        Logger.Info($"Enermy[{_id}] Dead.");
    }
    
    private Vector2 _findNxtPos()
    {
        if (!IsAlive.Value) return Position;
        Vector2 diffPos = _player.Position - Position;
        Vector2 nxtPos = Position;
        Vector2 direction;
        Logger.Debug($"Enermy[{_id}]: [{Position.X},{Position.Y}]");
        Logger.Debug($"diff: [{diffPos.X},{diffPos.Y}]");
        // FIXME: 아래 알고리즘을 좀 더 깔끔하게 할 방법 없을까?
        if (diffPos.X > 0 && diffPos.Y > 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y))
            {
                nxtPos += Vector2.Down;
                direction = Vector2.Down;
            }
            else
            {
                nxtPos += Vector2.Right;
                direction = Vector2.Right;
            }
        } 
        else if (diffPos.X < 0 && diffPos.Y > 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y))
            {
                nxtPos += Vector2.Up;
                direction = Vector2.Up;
            }
            else
            {
                nxtPos += Vector2.Right;
                direction = Vector2.Right;
            }
        } 
        else if (diffPos.X > 0 && diffPos.Y < 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y))
            {
                nxtPos += Vector2.Down;
                direction = Vector2.Down;
            }
            else
            {
                nxtPos += Vector2.Left;
                direction = Vector2.Left;
            }
        }
        else if (diffPos.X < 0 && diffPos.Y < 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y))
            {
                nxtPos += Vector2.Up;
                direction = Vector2.Up;
            }
            else
            {
                nxtPos += Vector2.Left;
                direction = Vector2.Left;
            }
        }
        else if (diffPos.X == 0 && diffPos.Y > 0)
        {
            nxtPos += Vector2.Right;
            direction = Vector2.Right;
        }
        else if (diffPos.X == 0 && diffPos.Y < 0)
        {
            nxtPos += Vector2.Left;
            direction = Vector2.Left;
        }
        else if (diffPos.X > 0 && diffPos.Y == 0)
        {
            nxtPos += Vector2.Down;
            direction = Vector2.Down;
        }
        else if (diffPos.X < 0 && diffPos.Y == 0)
        {
            nxtPos += Vector2.Up;
            direction = Vector2.Up;
        }
        else
        {
            nxtPos += Vector2.Down;
            direction = Vector2.Down;
        }
        // 돌, 나무, 보물 상자 충돌시 회피해가기.
        if (Map.IsObstacle(nxtPos) || Map.GetObject(nxtPos) is TreasureBox)
        {
            nxtPos = Position;
            if (direction == Vector2.Up) nxtPos += Vector2.Right;
            else if (direction == Vector2.Right) nxtPos += Vector2.Down;
            else if (direction == Vector2.Down) nxtPos += Vector2.Left;
            else nxtPos += Vector2.Up;
        }
        
        Logger.Debug($"nx[{_id}]: [{nxtPos.X},{nxtPos.Y}]");
        return nxtPos;
    }
    private void _autoMove()
    {
        Vector2 nxtPos = _findNxtPos();
        Move(nxtPos);
    }
}