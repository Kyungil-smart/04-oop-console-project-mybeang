using System.Runtime.CompilerServices;

public class Player : Charactor
{
    private const int MaxHealth = 5;
    private const int MaxDamage = 5;
    private const int MaxRange = 7;
    private const int MaxMana = 5;
    
    private ObservableProperty<int> _manaPoint = new ObservableProperty<int>();
    private ObservableProperty<int> _range = new ObservableProperty<int>();
    private ObservableProperty<int> _damage = new ObservableProperty<int>();
    public Player() => Init();
    public Tile[,] Map { get; set; }
    private TreasureBox _treasureBox;
    
    public override void Init()
    {
        _maxHealth.Value = 1;
        _curHealth.Value = 1;
        _damage.Value = 1;
        _range.Value = 4;
        
        Symbol = 'P';
        Type = GameObjectType.Chracter;
        Color = ConsoleColor.Blue;
    }

    protected override void Move(Vector2 direction)
    {
        Vector2 nextPos = Position + direction;
        if (nextPos.X >= Map.GetLength(0) || nextPos.Y >= Map.GetLength(1) ||
            nextPos.X < 0 || nextPos.Y < 0)
        {
            return;
        }

        GameObject nextTileObject = Map[nextPos.Y, nextPos.X].OnTileObject;
        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
            }
        }
        
        Logger.Debug($"player pos: [{Position.X}, {Position.Y}]");
        Map[Position.Y, Position.X].StepOff();
        Map[nextPos.Y, nextPos.X].StepOn(this);
        Position = nextPos;
    }

    private void ControlPlayer()
    {
        Vector2 direction = new Vector2(0, 0);
        if (InputManager.GetKey(ConsoleKey.LeftArrow))
            direction = Vector2.Left;
        
        if (InputManager.GetKey(ConsoleKey.RightArrow))
            direction = Vector2.Right;
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
            direction = Vector2.Up;
        
        if (InputManager.GetKey(ConsoleKey.DownArrow)) 
            direction = Vector2.Down;
        
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
            ControlPlayer();
        
        // 보물함 내 제어시 플레이어와 적은 paused 상태.
        else if (!GameManager.IsPaused)
            ControlItemSelect();
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
        if (_damage.Value >= MaxDamage) return;
        _damage.Value += point;
    }
    public void PlusMaxRange(int point)
    {
        if (_range.Value >= MaxRange) return;
        _range.Value += point;
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
    
}