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

public class Charactor : GameObject
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