public class Grass : GameObject
{
    public Grass() => Init();
    public override void Init()
    {
        Color = ConsoleColor.Green;
        Symbol = ',';
    }
}