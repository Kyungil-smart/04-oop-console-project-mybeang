public class IncHp : Item
{
    public IncHp() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "HP +1";
        Description = "Increase Max Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusMaxHp();
        _player = null;
    }
}