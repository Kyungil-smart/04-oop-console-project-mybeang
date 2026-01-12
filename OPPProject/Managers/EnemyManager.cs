public class EnemyManager
{
    public List<Enemy> Enemies;
    public int PopUpEnemyIndex;
    public int CurEnemiesOnMap;
    public int MaxEnemiesOnMap;
    private int totalCnt;
    private int aliveCnt;
    public ObservableProperty<(int alive, int total)> EnemyState = new();

    public void Setting(Player player, TopUI ui)
    {
        Enemies = new List<Enemy>();
        List<int> enemyLevels = csvToData.GetEnemy(SceneManager.StageNumber);
        MaxEnemiesOnMap = enemyLevels[0];
        for (int i = 1; i < enemyLevels.Count - 1; i++)
        {
            Enemy e = new Enemy(player, i);
            e.SetLevel(enemyLevels[i]);
            e.Map = player.Map;
            e.IsAlive.AddListener(EnemyIsDead);
            
            Enemies.Add(e);
        }
        totalCnt = Enemies.Count;
        aliveCnt = 0;
        PopUpEnemyIndex = 0;
        EnemyState.Value = EnemyState.Value with { total = Enemies.Count };
        EnemyState.AddListener(ui.RemainEnemy);
    }

    public void Update()
    {
        foreach (Enemy e in Enemies)
            e.Update();
    }
    
    public void EnemyIsDead(bool alive)
    {
        if (!alive)
        {
            CurEnemiesOnMap--;
            aliveCnt--;
        }
    }
    
    public void Clear()
    {
        Enemies.Clear();
        PopUpEnemyIndex = 0;
        CurEnemiesOnMap = 0;
        MaxEnemiesOnMap = 0;
    }
    
    public bool IsAllKilled() => totalCnt - aliveCnt == totalCnt;
    
    public void PopUpEnemy()
    {
        if (CurEnemiesOnMap >= MaxEnemiesOnMap) return;
        if (PopUpEnemyIndex >= totalCnt) return;
        Enemies[PopUpEnemyIndex++].PopUp();
        CurEnemiesOnMap++;
        aliveCnt++;
        EnemyState.Value = EnemyState.Value with { alive = aliveCnt };
    }
}