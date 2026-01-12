public class Stone : GameObject, ICrashBullet
{
    public Stone() => Init();
    public override void Init()
    {
        Symbol = 'S';
        Color = ConsoleColor.Gray;
    }
    public void CrashBullet(Bullet bullet)
    {
        bullet.Remove();
    }
}