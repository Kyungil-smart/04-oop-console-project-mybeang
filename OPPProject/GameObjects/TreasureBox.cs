public class TreasureBox : GameObject
{
    private Item[] _items = new Item[2];
    private Item _choicedItem;
    public MenuList _itemMenu = new();
    public PlayerCharactor Owner;

    public TreasureBox()
    {
        Init();
    }

    private Item GetItem()
    {
        Item _item;
        Random rnd = new Random();
        Item[] GivenItems = new Item[]
        {
            new Potion()
        };
        while (true)
        {
            _item = GivenItems[rnd.Next(0, GivenItems.Length)];
            if (_choicedItem == null)
            {
                _choicedItem = _item;
                return _item;
            }
            if (_choicedItem != _item)
            {
                _choicedItem = null;
                return _item;
            }
        }
    }
    
    public override void Init()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i] = GetItem();    
        }
        foreach (var item in _items)
        {
            item.Owner = Owner;
            _itemMenu.Add(item.Name, Use);
        }
    }
    
    public void Use()
    {
        _items[_itemMenu.CurrentIndex].Use();
        _items = [];
        _itemMenu.RemoveAll();
    }
    
    public void Render()
    {
        _itemMenu.Render(Owner.Position + Vector2.Right);
    }

    public void Select()
    {
        _itemMenu.Select();
    }

    public void CursorUp()
    {
        _itemMenu.CursorUp();
    }

    public void CursorDown()
    {
        _itemMenu.CursorDown();
    }
}