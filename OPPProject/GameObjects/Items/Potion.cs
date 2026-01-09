public class Potion : Item, IInteractable
{
    public Potion() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "Heal Potion";
        Description = "Heal 1 Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        Owner.Heal();
        Owner = null;
    }

    public void Interact(Player player)
    {
        player.OpenTreasureBox(this);
    }
}