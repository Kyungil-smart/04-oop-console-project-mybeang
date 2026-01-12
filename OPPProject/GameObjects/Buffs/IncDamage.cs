public class IncDamage : Buff
{
    public IncDamage() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "데미지를 1 증가";
        Description = "Increase the Damage to 1 point.";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusDamage();
        _player = null;
    }
}