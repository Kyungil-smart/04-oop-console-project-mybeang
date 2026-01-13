// 버프를 관리하는 객체. GameObject 로써 맵위에 팝업되어야함.
public class BuffBox : GameObject, IInteractable, INotPlaceable
{
    private Buffs[] _items = new Buffs[2];
    private Buffs _choicedBuffs;
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
        // 버프가 중첩으로 발생할 수 있음!!
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
    
    // 기본 버프 중 랜덤으로 고르기. 
    private Buffs ChoiceItem()
    {
        Random rnd = new Random();
        Buffs[] GivenItems = new Buffs[]
        {
            new Heal(), new IncHp(), new IncRange(), new IncDamage()
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

    // 플레이어 접촉시
    public void Interact(Player player)
    {
        GameManager.IsPaused = true;
        IsOpenedBox = true;
        player.BuffBx = this;
        Render();
    }
}