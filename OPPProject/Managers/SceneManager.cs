public enum SceneName
{
    Void,
    Title,
    GameOver,
    Story,
    Credits,
    Stage,
    Log,
}


public static class SceneManager
{
    public static Action OnChangeScene;
    public static Scene Current { get; private set; }
    private static Scene _prev;
    public static int StageNumber { get; set; }
    
    private static Dictionary<SceneName, Scene> _scenes = new Dictionary<SceneName, Scene>();

    public static void Add(SceneName key, Scene scene)
    {
        if (_scenes.ContainsKey(key)) return;
        _scenes.Add(key, scene);
    }
    public static void ChangePrevScene()
    {
        Change(_prev);
    }

    private static void _change(Scene next)
    {
        if (next == Current) return;
        Current?.Exit();
        next.Enter();
        
        _prev = Current;
        Current = next;
        OnChangeScene?.Invoke();
    }
    
    public static void Change(SceneName key)
    {
        if (!_scenes.ContainsKey(key)) return;
        
        Scene next = _scenes[key];
        _change(next);
    }
    public static void Change(Scene scene)
    {
        Scene next = scene;
        _change(next);
    }

    public static void Update()
    {
        Current?.Update();
    }
    
    public static void Render()
    {
        Current?.Render();
    }

    public static SceneName GetCurrentSceneName()
    {
        foreach (SceneName sceneName in _scenes.Keys)
        {
            if (_scenes[sceneName] == Current) return sceneName;
        }
        return SceneName.Void;
    }
}