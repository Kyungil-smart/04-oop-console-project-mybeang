public class Grass : GameObject
{
    public Grass() => Init();
    public override void Init()
    {
        Type = GameObjectType.Chracter;
        Color = ConsoleColor.Green;
        Symbol = ',';
    }
}