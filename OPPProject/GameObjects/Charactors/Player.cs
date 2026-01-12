public class Player : Charactor
{
    private const int MaxHealth = 5;
    private const int MaxDamage = 3;
    private const int MaxRange = 7;
    private const int MaxMana = 5;
    
    private ObservableProperty<int> _manaPoint = new ObservableProperty<int>();
    private int _range;
    private int _damage;
    public Player() => Init();
    public TreasureBox InteractableTb;
    private Direction _direction;
    private List<Bullet> _bullets;

    public override void Init()
    {
        Health.Value = new Hp(1, 1);
        _damage = 1;
        _range = 4;
        _direction = Direction.Down;
        
        Symbol = 'P';
        Color = ConsoleColor.Blue;
        _bullets = new();
    }

    protected override void Move(Vector2 direction)
    {
        Vector2 nextPos = Position + direction;
        if (Map.IsOutOfMap(nextPos)) return;
        if (Map.IsObstacle(nextPos)) return;

        GameObject nextTileObject = Map.GetObject(nextPos);
        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
            }
        }

        if (GameManager.IsPaused) return;
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
            _direction = Direction.Left;
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            direction = Vector2.Right;
            _direction = Direction.Right;
        }

        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            direction = Vector2.Up;
            _direction = Direction.Up;
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            direction = Vector2.Down;
            _direction = Direction.Down;
        }

        if (InputManager.GetKey(ConsoleKey.Z))
        {
            ShootBullet();
        }
        
        if (Map != null) Move(direction);
        Logger.Debug($"Player:[{Position.X},{Position.Y}]");
    }

    private void ControlItemSelect()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            InteractableTb.CursorUp();
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
            InteractableTb.CursorDown();
        
        if (InputManager.GetKey(ConsoleKey.Enter))
            InteractableTb.Select();
    }
    
    public override void Update()
    {
        if (!GameManager.IsPaused)
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].Range <= 0)
                {
                    _bullets[i].Disappear();
                    _bullets.RemoveAt(i--);
                    continue;
                }
                _bullets[i].Move();
                Thread.Sleep(100);
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
        if (_damage >= MaxDamage) return;
        _damage++;
    }
    public void PlusMaxRange()
    {
        if (_range >= MaxRange) return;
        _range++;
    }
    public void PlusMaxHp()
    {
        if (Health.Value.Total >= MaxHealth) return;
        Health.Value = Health.Value with { Total = Health.Value.Total + 1 };
    }
    public void HealMana(int point)
    {
        // FIXME: 나중 구현
        _manaPoint.Value += point;
    }

    public void ShootBullet()
    {
        Vector2 bulletPos = new Vector2(Position.X, Position.Y);
        
        if (_direction == Direction.Up) 
            bulletPos += Vector2.Up;
        else if (_direction == Direction.Down) 
            bulletPos += Vector2.Down;
        else if (_direction == Direction.Left) 
            bulletPos += Vector2.Left;
        else 
            bulletPos += Vector2.Right;
        
        _bullets.Add(new Bullet(this, bulletPos, _damage, _range, _direction));
    }
    
    public void TakeDamage()
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - 1 };
    }
}