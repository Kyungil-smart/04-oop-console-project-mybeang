public class Player : Charactor
{
    private const int MaxHealth = 5;
    private const int MaxDamage = 5;
    private const int MaxRange = 7;
    private const int MaxMana = 5;
    
    private ObservableProperty<int> _manaPoint = new ObservableProperty<int>();
    private int _range;
    private int _damage;
    public Player() => Init();
    public Tile[,] Map { get; set; }
    private TreasureBox _treasureBox;
    private Direction _direction;
    private List<Bullet> _bullets;
    
    public override void Init()
    {
        _maxHealth.Value = 1;
        _curHealth.Value = 1;
        _damage = 1;
        _range = 4;
        _direction = Direction.Down;
        
        Symbol = 'P';
        Type = GameObjectType.Chracter;
        Color = ConsoleColor.Blue;
        _bullets = new();
    }

    protected override void Move(Vector2 direction)
    {
        Vector2 nextPos = Position + direction;
        if (IsOutOfMap(Map, nextPos))
            return;

        GameObject nextTileObject = Map[nextPos.Y, nextPos.X].OnTileObject;
        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
            }
        }
        
        Map[Position.Y, Position.X].StepOff();
        Map[nextPos.Y, nextPos.X].StepOn(this);
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

        if (InputManager.GetKey(ConsoleKey.A))
        {
            ShootBullet();
        }
        
        if (Map != null) Move(direction);
    }

    private void ControlItemSelect()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            _treasureBox.CursorUp();
        
        if (InputManager.GetKey(ConsoleKey.DownArrow))
            _treasureBox.CursorDown();
        
        if (InputManager.GetKey(ConsoleKey.Enter))
            _treasureBox.Select();
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
        else if (GameManager.IsPaused)
        {
            ControlItemSelect();
        }
    }

    public void OpenTreasureBox(Item item)
    {
        _treasureBox.Owner = this;
        GameManager.IsPaused = true;
        _treasureBox.Render();
    }

    public void Render()
    {
        // _treasureBox.Render();
        
    }

    public void DrawHealthGauge()
    {
        // FIXME: 나중에 구현
        // 왼쪽위 UI 에 그릴껀데...?
    }

    public void Heal(int point)
    {
        if (_curHealth.Value == _maxHealth.Value) return;
        _curHealth.Value += point;
    }
    public void PlusDamage(int point)
    {
        if (_damage >= MaxDamage) return;
        _damage += point;
    }
    public void PlusMaxRange(int point)
    {
        if (_range >= MaxRange) return;
        _range += point;
    }
    public void PlusMaxHp(int point)
    {
        if (_maxHealth.Value >= MaxHealth) return;
        _maxHealth.Value += point;
    }
    public void HealMana(int point)
    {
        // FIXME: 나중 구현
        _manaPoint.Value += point;
    }

    public void ShootBullet()
    {
        // 일단 가던 방향으로만 쏘자.
        // 일단 특정 키 입력시 쏘게 하자.
        Vector2 bulletPos = new Vector2(Position.X, Position.Y);
        switch (_direction)
        {
            case Direction.Up:
                bulletPos += Vector2.Up;
                break;
            case Direction.Down:
                bulletPos += Vector2.Down;
                break;
            case Direction.Left:
                bulletPos += Vector2.Left;
                break;
            default:  // Right
                bulletPos += Vector2.Right;
                break;
        }
        _bullets.Add(new Bullet(this, bulletPos, _damage, _range, _direction));
    }
}