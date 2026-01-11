public class IncRange : Item
{
    public IncRange() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "Range +1";
        Description = "Increase Shoot Range";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusMaxRange();
        _player = null;
    }
}