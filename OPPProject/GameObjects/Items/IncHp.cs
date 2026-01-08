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
        Owner.PlusMaxHp(1);
        Owner = null;
    }

    public void Interact(PlayerCharactor player)
    {
        player.OpenTreasureBox(this);
    }
}