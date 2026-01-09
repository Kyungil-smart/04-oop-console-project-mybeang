public class IncHp : Item, IInteractable
{
    public IncHp() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "";
        Description = "Increase Max Health Point";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        Owner.PlusMaxHp();
        Owner = null;
    }

    public void Interact(Player player)
    {
        player.OpenTreasureBox(this);
    }
}