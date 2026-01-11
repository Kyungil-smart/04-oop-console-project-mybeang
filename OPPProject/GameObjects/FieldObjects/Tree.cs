public class Tree : GameObject, ICrashBullet
{
    public override void Init()
    {
        Symbol = 'T';
        Color = ConsoleColor.White;
    }


    public void CrashBullet(Bullet bullet)
    {
        bullet.Disappear();
    }
}