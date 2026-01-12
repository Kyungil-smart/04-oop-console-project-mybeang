public class Enermy : Charactor, IInteractable, ICrashBullet
{
    private int _level;
    private Player _player;
    public ObservableProperty<bool> IsAlive = new();
    
    public Enermy(Player player)
    {
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
        bullet.Disappear();
    }

    public override void Update()
    {
        _autoMove();
    }

    private void _autoMove()
    {
        if (!IsAlive.Value) return;
        Vector2 diffPos = _player.Position - Position;
        Vector2 nxtPos = Position;
        Logger.Debug($"Enermy: [{Position.X},{Position.Y}]");
        Logger.Debug($"diff: [{diffPos.X},{diffPos.Y}]");
        // FIXME: 아래 알고리즘을 좀 더 깔끔하게 할 방법 없을까?
        if (diffPos.X > 0 && diffPos.Y > 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y)) nxtPos += Vector2.Up;
            else nxtPos += Vector2.Left;
        } 
        else if (diffPos.X < 0 && diffPos.Y > 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y)) nxtPos += Vector2.Up;
            else nxtPos += Vector2.Right;
        } 
        else if (diffPos.X > 0 && diffPos.Y < 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y)) nxtPos += Vector2.Down;
            else nxtPos += Vector2.Left;
        }
        else if (diffPos.X < 0 && diffPos.Y < 0)
        {
            if (Math.Abs(diffPos.X) >= Math.Abs(diffPos.Y)) nxtPos += Vector2.Down;
            else nxtPos += Vector2.Right;
        }
        else if (diffPos.X == 0 && diffPos.Y > 0)
        {
            nxtPos += Vector2.Down;
        }
        else if (diffPos.X == 0 && diffPos.Y < 0)
        {
            nxtPos += Vector2.Up;
        }
        else if (diffPos.X > 0 && diffPos.Y == 0)
        {
            nxtPos += Vector2.Left;
        }
        else if (diffPos.X < 0 && diffPos.Y == 0)
        {
            nxtPos += Vector2.Right;
        }
        else
        {
            nxtPos += Vector2.Down;
        }
        
        Logger.Debug($"nx: [{nxtPos.X},{nxtPos.Y}]");
        Move(nxtPos);
    }
    
    protected override void Move(Vector2 nxPos)
    {
        if (Map.IsOutOfMap(nxPos)) return;
        if (Map.IsObstacle(nxPos)) return;
        
        if (GameManager.IsPaused) return;
        Map.UnsetObject(this);
        Map.SetObject(nxPos, this);
        Position = nxPos;
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
    }
}