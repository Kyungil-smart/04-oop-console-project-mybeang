public class Potion : Item, IInteractable
{
    public Potion() => Init();
    
    public override void Init()
    {
        Id = 0;
        Name = "Heal Potion";
        Description = "Heal HP {}";
        Symbol = 'H';
    }

    public override void Use()
    {
        Logger.Info("포션 사용");
        // Owner.Heal(1);
        Owner = null;
    }

    public void Interact(PlayerCharactor player)
    {
        player.OpenTreasureBox(this);
    }
}