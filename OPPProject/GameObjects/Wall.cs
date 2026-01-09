public class Wall : GameObject, ICrashBullet
{
    public override void Init()
    {
        Symbol = 'W';
        Color = ConsoleColor.White;
    }


    public void CrashBullet(Bullet bullet)
    {
        bullet.Disappear();
    }
}