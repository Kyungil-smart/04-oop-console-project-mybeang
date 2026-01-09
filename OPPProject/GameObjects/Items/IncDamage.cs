public class IncDamage : Item, IInteractable
{
    public IncDamage() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "";
        Description = "Increase the Damage to 1 point.";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        Owner.PlusDamage();
        Owner = null;
    }

    public void Interact(Player player)
    {
        player.OpenTreasureBox(this);
    }
}