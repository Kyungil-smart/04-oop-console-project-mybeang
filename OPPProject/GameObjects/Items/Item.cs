public enum ItemType
{
    Potion,
    Weapon,
    Ammor
}

public abstract class Item : GameObject
{
    public int Id  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PlayerCharactor Owner { get; set; }
    
    public abstract void Use();
    
    public void PrintInfo()
    {
        
    }
}