public class Potion : Item
{
    public Potion() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "잃어버린 HP 1 증가";
        Description = "Heal 1 Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.Heal();
        _player = null;
    }
}