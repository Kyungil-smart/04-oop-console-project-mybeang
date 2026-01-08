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
        Owner.PlusDamage(1);
        Owner = null;
    }

    public void Interact(PlayerCharactor player)
    {
        player.OpenTreasureBox(this);
    }
}