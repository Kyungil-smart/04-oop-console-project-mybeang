public struct Hp
{
    public int Current;
    public int Total;
    public Hp(int current, int total)
    {
        Current = current;
        Total = total;
    }
}

// 플레이어 및 적 개체를 위한 부모 객체.
public class Charactor : GameObject, INotPlaceable
{
    public ObservableProperty<Hp> Health = new ObservableProperty<Hp>();
    public Charactor() => Init();
    public override void Init()
    {
        Symbol = 'C';
    }

    protected virtual void Move(Vector2 direction) {}

    public virtual void Update(){}
}