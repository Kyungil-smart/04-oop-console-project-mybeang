public class Enemy : Charactor, IInteractable, ICrashBullet
{
    private int _level;
    private Player _player;
    private int _aliveTime;
    public ObservableProperty<bool> IsAlive = new();
    
    public Enemy(Player player)
    {
        _player = player;
        IsAlive.Value = false;
        Init();
        _aliveTime = 0;
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
        Logger.Info($"{Health.Value.Total}/{_level}/");
    } 
    
    public void TakeDamage(int damage)
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - damage };
        if (Health.Value.Current <= 0)
            Dead();
    }
    
    public void Interact(Player player)
    {
        // 플레이어가 적에게 돌진 했을 때 데미지를 입는다.
        player.TakeDamage();
    }

    public void CrashBullet(Bullet bullet)
    {
        // 몬스터가 데미지를 입는다.
        TakeDamage(bullet.Damage);
        bullet.Remove();
    }

    public override void Update()
    {
        if (IsAlive.Value)
            AutoMove();
    }
    
    protected override void Move(Vector2 nxtPos)
    {
        if (_aliveTime % 5 == 4)
        {
            // 밖으로 나가지 말것
            if (Map.IsOutOfMap(nxtPos)) return;
            if (nxtPos == _player.Position)
            {
                // 플레이어에게 데미지를 준 후 위치 고수.
                _player.TakeDamage();
                return;
            }
            // 적끼리 뭉치지 말것
            if (Map.GetObject(nxtPos) is Enemy) return;
            // Pause 시 가만히 있기
            if (GameManager.IsPaused) return;
            Map.UnsetObject(this);
            Map.SetObject(nxtPos, this);
            Position = nxtPos;
        }
        _aliveTime++;
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
        _aliveTime = int.MaxValue;
    }
    
    private void AutoMove()
    {
        Vector2 nxtPos = FindNxtPos();
        Move(nxtPos);
    }
    
    private Vector2 FindNxtPos()
    {
        if (!IsAlive.Value) return Position;
        Vector2 diffPos = _player.Position - Position;
        Vector2 nxtPos = Position;
        Vector2 direction;
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
        if (Map.IsObstacle(nxtPos) || Map.GetObject(nxtPos) is BuffBox)
        {
            nxtPos = Position;
            if (direction == Vector2.Up) nxtPos += Vector2.Right;
            else if (direction == Vector2.Right) nxtPos += Vector2.Down;
            else if (direction == Vector2.Down) nxtPos += Vector2.Left;
            else nxtPos += Vector2.Up;
        }
        return nxtPos;
    }
}