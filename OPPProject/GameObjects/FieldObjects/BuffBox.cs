public class BuffBox : GameObject, IInteractable
{
    private Buff[] _items = new Buff[2];
    private Buff _choicedBuff;
    public MenuList _itemMenu;
    private Player _player;
    public bool IsOpenedBox;

    public BuffBox(Vector2 position, Player player)
    {
        Position = position;
        _player = player;
        Init();
    }

    public override void Init()
    {
        _itemMenu = new MenuList(title: "> 버프 1개 선택");
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
    
    private Buff ChoiceItem()
    {
        Random rnd = new Random();
        Buff[] GivenItems = new Buff[]
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
        _itemMenu.Render(2, 3);
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
        player.BuffBx = this;
        Render();
    }
}