public abstract class Buff : GameObject
{
    public int Id  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    protected Player _player { get; set; }
    
    public abstract void Use();
    
    public void SetPlayer(Player player) => _player = player;
    
    public void PrintInfo()
    {
        
    }
}