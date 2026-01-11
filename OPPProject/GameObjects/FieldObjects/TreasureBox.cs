public class TreasureBox : GameObject, IInteractable
{
    private Item[] _items = new Item[2];
    private Item _choicedItem;
    public MenuList _itemMenu = new();
    private Player _player;
    public bool IsOpenedBox;

    public TreasureBox(Vector2 position, Player player)
    {
        Position = position;
        _player = player;
        Init();
    }

    public override void Init()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = ChoiceItem();
            _items[i].SetPlayer(_player);
            _itemMenu.Add(_items[i].Name, Use);
        }
        Map = _player.Map;
        Map.SetObject(this);
        Symbol = 'B';
        Color = ConsoleColor.Cyan;
        IsOpenedBox = false;
    }
    
    private Item ChoiceItem()
    {
        Random rnd = new Random();
        Item[] GivenItems = new Item[]
        {
            new Potion(), new IncHp(), new IncRange(), new IncDamage()
        };
        return GivenItems[rnd.Next(0, GivenItems.Length)];
    }
    
    public void Use()
    {
        _items[_itemMenu.CurrentIndex].Use();
        _items = [];
        _itemMenu.RemoveAll();
        GameManager.IsPaused = false;
    }
    
    public void Render()
    {
        _itemMenu.Render(_player.Position + Vector2.Right);
    }

    public void Select()
    {
        _itemMenu.Select();
        IsOpenedBox = false;
        GameManager.IsPaused = false;
        Map.UnsetObject(this);
    }

    public void CursorUp()
    {
        _itemMenu.CursorUp();
    }

    public void CursorDown()
    {
        _itemMenu.CursorDown();
    }

    public void Interact(Player player)
    {
        GameManager.IsPaused = true;
        IsOpenedBox = true;
        player.InteractableTb = this;
        Render();
    }
}