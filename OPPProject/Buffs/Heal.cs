public class Heal : Buffs
{
    public Heal() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "HP 1 회복";
        Description = "Heal 1 Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.Heal();
        _player = null;
    }
}