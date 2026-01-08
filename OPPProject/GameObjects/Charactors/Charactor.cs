

public class Charactor : GameObject
{
    protected ObservableProperty<int> _curHealth = new ObservableProperty<int>();
    protected ObservableProperty<int> _maxHealth = new ObservableProperty<int>();
    public Charactor() => Init();
    public override void Init()
    {
        Symbol = 'C';
    }

    protected virtual void Move(Vector2 direction) {}

    public virtual void Update(){}
}