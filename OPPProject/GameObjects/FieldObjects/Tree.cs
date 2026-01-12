public class Tree : GameObject, ICrashBullet
{
    public Tree() => Init();
    public override void Init()
    {
        Symbol = 'T';
        Color = ConsoleColor.DarkGreen;
    }


    public void CrashBullet(Bullet bullet)
    {
        bullet.Remove();
    }
}