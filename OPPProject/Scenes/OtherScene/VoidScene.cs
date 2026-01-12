public class VoidScene : Scene
{
    // Stage 중간 넘김을 위한 아무것도 없는 씬.
    public override void Enter() {}

    public override void Update()
    {
        SceneManager.Change(SceneName.Stage);
    }

    public override void Render() {}

    public override void Exit() {}
}