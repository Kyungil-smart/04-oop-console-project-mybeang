using System.Runtime.CompilerServices;

public class PlayerCharactor : Charactor
{
    public ObservableProperty<int> Health = new ObservableProperty<int>();
    public ObservableProperty<int> Mana = new ObservableProperty<int>();
    public PlayerCharactor() => Init();
    public Tile[,] Map { get; set; }
    private TreasureBox _treasureBox;
    
    public override void Init()
    {
        Symbol = 'P';
        Type = GameObjectType.Chracter;
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
        if (Map != null) Move(direction);
    }

    private void ControlItemSelect()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _treasureBox.CursorUp();
        }
        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _treasureBox.CursorDown();
        }
        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _treasureBox.Select();
        }
    }
    
    public override void Update()
    {
        if (!GameManager.IsPaused)
        {
            ControlPlayer();
        } 
        else if (!GameManager.IsPaused)
        {
            ControlItemSelect();
        }
    }

    public void OpenTreasureBox(Item item)
    {
        _treasureBox.Owner = this;
        _treasureBox.Render();
    }

    public void Render()
    {
        _treasureBox.Render();
    }

    public void DrawHealthGauge()
    {
        
    }

    public void Heal(int point)
    {
        Health.Value += point;
    }
}