// 적 
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

    // 레벨에 따른 체력 초기화.
    public void SetLevel(int level)
    {
        _level = level;  
        Health.Value = new Hp(2 * _level, 2 * _level);
        Logger.Info($"{Health.Value.Total}/{_level}/");
    } 
    
    // 데미지 입힘.
    public void TakeDamage(int damage)
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - damage };
        if (Health.Value.Current <= 0)
            Dead();
    }
    
    // 플레이어->적 일때, 플레이어가 데미지를 입는다.
    public void Interact(Player player)
    {
        player.TakeDamage();
    }

    // 총알에 맞으면 적은 데미지를 입는다.
    public void CrashBullet(Bullet bullet)
    {
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
        // 0.5초마다 1보. 
        if (_aliveTime % 5 == 4)
        {
            // 밖으로 나가지 말것
            if (Map.IsOutOfMap(nxtPos)) return;
            // 플레이어에게 데미지를 준 후 위치 고수.
            if (nxtPos == _player.Position)
            {
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

    // 적 관리 객체에서 일괄적으로 랜더링.
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
        _aliveTime = -1;
    }
    
    // 플레이어를 향해 자동 이동
    private void AutoMove()
    {
        Vector2 nxtPos = FindNxtPos();
        Move(nxtPos);
    }
    
    // 플레이어 위치를 통한 다음 위치 정보 계산
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