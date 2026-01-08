public class Inventory
{
    private List<Item> _items = new();
    private int _maxCount = 10;
    public bool IsOpened = false;
    public MenuList _itemMenu = new();
    public PlayerCharactor Owner;
    
    public Inventory(PlayerCharactor p) => Owner = p;
    
    public void Add(Item item)
    {
        if (_items.Count >= _maxCount)
        {
            Logger.Debug($"가방이 꽉참. {item.Name} 획득 불가");
            return;
        }
        item.Owner = Owner;
        _items.Add(item);
        _itemMenu.Add(item.Name, Use);
    }
    public void Remove()
    {
        _items.RemoveAt(_itemMenu.CurrentIndex);
        _itemMenu.RemoveCurrentIndex();
    }

    public void Use()
    {
        _items[_itemMenu.CurrentIndex].Use();
        Remove();
    }
    
    public void Render()
    {
        if (IsOpened) _itemMenu.Render(15, 1);
    }

    public void Select()
    {
        if (IsOpened) _itemMenu.Select();
    }

    public void CursorUp()
    {
        if (IsOpened) _itemMenu.CursorUp();
    }

    public void CursorDown()
    {
        if (IsOpened) _itemMenu.CursorDown();
    }
}