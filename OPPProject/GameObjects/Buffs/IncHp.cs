public class IncHp : Buff
{
    public IncHp() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "HP를 1 증가";
        Description = "Increase Max Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        _player.PlusMaxHp();
        _player = null;
    }
}