public class Enermy : Charactor, IInteractable, ICrashBullet
{
    private int _level;
    public Enermy() => Init();
    
    public void Init()
    {
        Symbol = 'E';
        Color = ConsoleColor.DarkRed;
    }

    public void SetLevel(int level)
    {
        _level = level;  
        Health.Value = new Hp(2 * _level, 2 * _level);
    } 
    
    public void TakeDamage(int damage)
    {
        Health.Value = Health.Value with { Current = Health.Value.Current - damage };
    }
    
    public void Interact(Player player)
    {
        player.TakeDamage();
    }

    public void CrashBullet(Bullet bullet)
    {
        TakeDamage(bullet.Damage);
    }

    public override void Update()
    {
        
    }
    
    protected override void Move(Vector2 direction)
    {
        
    }

    public void Render()
    {
        
    }

    private void FindPlayer(Vector2 playerPosition)
    {
    }

    public void Dead()
    {
        Map[Position.Y, Position.X].StepOff();
    }
}