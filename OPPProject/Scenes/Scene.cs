
public abstract class Scene
{
    public int Width = 100;
    public int Height = 100;
    public int RenderWidth = 20;
    public int RenderHeight = 20;
    
    public abstract void Enter();
    public abstract void Update();
    public abstract void Render();
    public abstract void Exit();
}