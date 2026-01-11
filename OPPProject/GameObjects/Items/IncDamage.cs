public class IncDamage : Item
{
    public IncDamage() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "Damage +1";
        Description = "Increase the Damage to 1 point.";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusDamage();
        _player = null;
    }
}