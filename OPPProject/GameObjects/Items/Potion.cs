public class Potion : Item
{
    public Potion() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "Heal +1";
        Description = "Heal 1 Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.Heal();
        _player = null;
    }
}