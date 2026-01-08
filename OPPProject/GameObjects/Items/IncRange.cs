public class IncRange : Item, IInteractable
{
    public IncRange() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "";
        Description = "Increase Shoot Range";
    }

    public override void Use()
    {
        Logger.Debug($"[ITEM] {Name}: {Description}");
        Owner.PlusMaxRange(1);
        Owner = null;
    }

    public void Interact(PlayerCharactor player)
    {
        player.OpenTreasureBox(this);
    }
}