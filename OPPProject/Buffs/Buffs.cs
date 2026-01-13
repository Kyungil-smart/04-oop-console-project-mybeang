// 버프를 위한 추상화 클래스
public abstract class Buffs
{
    public int Id  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    protected Player _player { get; set; }

    public abstract void Init();
    public abstract void Use();
    
    public void SetPlayer(Player player) => _player = player;
}