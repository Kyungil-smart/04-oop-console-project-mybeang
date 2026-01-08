using System.Runtime.CompilerServices;

public class PlayerCharactor : Charactor
{
    public ObservableProperty<int> Health = new ObservableProperty<int>();
    public ObservableProperty<int> Mana = new ObservableProperty<int>();
    public PlayerCharactor() => Init();
    public Tile[,] Map { get; set; }
    private Inventory _inventory;
    
    public override void Init()
    {
        Symbol = 'P';
        Type = GameObjectType.Chracter;
        _inventory = new(this);
        _inventory.IsOpened = false;
        
        _inventory.Add(new Potion());
        _inventory.Add(new Potion());
        _inventory.Add(new Potion());
        _inventory.Add(new Potion());
    }

    private void Move(Vector2 direction)
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
        
        Map[Position.Y, Position.X].OnTileObject = null;
        Map[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;
    }

    private void ControlPlayer()
    {
        Vector2 direction = new Vector2(0, 0);
        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            direction = Vector2.Left;
        }
        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            direction = Vector2.Right;
        }
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            direction = Vector2.Up;
        }
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            direction = Vector2.Down;
        }
        if (InputManager.GetKey(ConsoleKey.I))
        {
            _inventory.IsOpened = true;
            Render();
        }
        if (Map != null) Move(direction);
    }

    private void ControlInventory()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _inventory.CursorUp();
        }
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _inventory.CursorDown();
        }
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _inventory.Select();
        }
        if (InputManager.GetKey(ConsoleKey.I))
        {
            _inventory.IsOpened = false;
        }
    }
    
    public override void Update()
    {
        if (!_inventory.IsOpened)
        {
            ControlPlayer();
        } 
        else if (_inventory.IsOpened)
        {
            ControlInventory();
        }
    }

    public void GetItem(Item item)
    {
        _inventory.Add(item);
    }

    public void Render()
    {
        _inventory.Render();
    }

    public void DrawHealthGauge()
    {
        
    }

    public void Heal(int point)
    {
        Health.Value += point;
    }
}