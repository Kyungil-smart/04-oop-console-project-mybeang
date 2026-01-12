public class IncRange : Buff
{
    public IncRange() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "사정거리를 1 증가";
        Description = "Increase Shoot Range";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusMaxRange();
        _player = null;
    }
}