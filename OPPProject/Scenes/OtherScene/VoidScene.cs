// Stage 넘김을 위해 중간에 지나가는 아무것도 없는 씬.
public class VoidScene : Scene
{
    public override void Enter() {}

    public override void Update()
    {
        SceneManager.Change(SceneName.Stage);
    }

    public override void Render() {}

    public override void Exit() {}
}