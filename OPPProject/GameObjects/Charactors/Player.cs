// 플레이어 케릭터
public class Player : Charactor
{
    private const int MaxHealth = 5;
    private const int MaxDamage = 3;
    private const int MaxRange = 10;
    private const int MaxMana = 5;
    
    private ObservableProperty<int> _manaPoint = new ObservableProperty<int>();
    public int Range;
    public int Damage;
    public Player() => Init();
    public BuffBox BuffBx;
    public Direction Direction;
    private List<Bullet> _bullets;

    // 플레이어 준비
    public override void Init()
    {
        Health.Value = new Hp(1, 1);
        Damage = 1;
        Range = 6;
        Direction = Direction.Down;
        Symbol = '▼';
        Color = ConsoleColor.Blue;
        _bullets = new();
    }

    // 플레이어 실제 움직임
    protected override void Move(Vector2 direction)
    {
        Vector2 nextPos = Position + direction;
        if (Map.IsOutOfMap(nextPos)) return;
        if (Map.IsObstacle(nextPos)) return;
        // Pause 상태에서 혹시 모를 플레이어 움직임을 위한 방어로직.
        if (GameManager.IsPaused) return;

        GameObject nextTileObject = Map.GetObject(nextPos);
        if (nextTileObject != null && nextTileObject is IInteractable)
            (nextTileObject as IInteractable).Interact(this);
        
        Map.UnsetObject(this);
        Map.SetObject(nextPos, this);
        Position = nextPos;
    }

    private void ControlPlayer()
    {
        Vector2 direction = new Vector2(0, 0);
        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            direction = Vector2.Left;
            Direction = Direction.Left;
            Symbol = '◀';
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            direction = Vector2.Right;
            Direction = Direction.Right;
            Symbol = '▶';
        }

        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            direction = Vector2.Up;
            Direction = Direction.Up;
            Symbol = '▲';
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            direction = Vector2.Down;
            Direction = Direction.Down;
            Symbol = '▼';
        }

        if (InputManager.GetKey(ConsoleKey.Z))
            ShootBullet();
        
        if (InputManager.GetKey(ConsoleKey.Q)) // 테스트용
            SceneManager.Change(SceneName.GameOver);
        
        if (Map != null) Move(direction);
    }

    private void ControlItemSelect()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            BuffBx.CursorUp();
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
            BuffBx.CursorDown();
        
        if (InputManager.GetKey(ConsoleKey.Enter))
            BuffBx.Select();
    }
    
    public override void Update()
    {
        if (!GameManager.IsPaused)
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].Range <= 0)
                {
                    _bullets[i].Remove();
                    _bullets.RemoveAt(i--);
                    continue;
                }
                _bullets[i].Move();
            }
            ControlPlayer();
        }
        
        // 보물함 내 제어시 플레이어와 적은 paused 상태.
        if (GameManager.IsPaused)
        {
            ControlItemSelect();
        }
    }

    public void Render() {}

    public void Heal()
    {
        if (Health.Value.Current == Health.Value.Total) return;
        Health.Value = Health.Value with { Current = Health.Value.Current + 1 };
    }
    
    public void PlusDamage()
    {
        if (Damage >= MaxDamage) return;
        Damage++;
    }
    
    public void PlusMaxRange()
    {
        if (Range >= MaxRange) return;
        Range++;
    }
    
    public void PlusMaxHp()
    {
        if (Health.Value.Total >= MaxHealth) return;
        Health.Value = Health.Value with { Total = Health.Value.Total + 1 };
    }
    
    // FIXME: 나중 구현
    public void HealMana(int point)
    {
        _manaPoint.Value += point;
    }

    public void ShootBullet()
    {
        Vector2 bulletPos = new Vector2(Position.X, Position.Y);
        
        if (Direction == Direction.Up) 
            bulletPos += Vector2.Up;
        else if (Direction == Direction.Down) 
            bulletPos += Vector2.Down;
        else if (Direction == Direction.Left) 
            bulletPos += Vector2.Left;
        else 
            bulletPos += Vector2.Right;
        
        _bullets.Add(new Bullet(this, bulletPos));
    }
    
    public void TakeDamage()
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - 1 };
    }
}