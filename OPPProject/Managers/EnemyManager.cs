// 적 관리 클래스
public class EnemyManager
{
    public List<Enemy> Enemies;  // 적 리스트
    public int PopUpEnemyIndex;  // 팝업된 적 Index
    public int CurEnemiesOnMap;  // 현재 맵 위 적 개체 수
    public int MaxEnemiesOnMap;  // 최대 맵 위 적 개체 수
    private int totalCnt;  // 최대 적 개체수
    private int aliveCnt;  // 토벌당하지 않은 적 개체수
    // 총 적 개체수 대비 남은 적 개체수 UI 표현을 위한 이벤트
    public ObservableProperty<(int alive, int total)> EnemyState = new();  

    // 초기 데이터 셋팅
    public void Setting(Player player)
    {
        Enemies = new List<Enemy>();
        // 적 csv 데이터를 List 화
        List<int> enemyLevels = csvToData.GetEnemy(SceneManager.StageNumber);
        MaxEnemiesOnMap = enemyLevels[0];
        for (int i = 1; i < enemyLevels.Count; i++)
        {
            Enemy e = new Enemy(player);
            e.SetLevel(enemyLevels[i]);
            e.Map = player.Map;
            // 적 토벌시 데이터 변환을 위한 리스너 추가
            e.IsAlive.AddListener(EnemyIsDead); 
            Enemies.Add(e);
        }
        totalCnt = Enemies.Count;
        aliveCnt = 0;
        PopUpEnemyIndex = 0;
        EnemyState.Value = EnemyState.Value with { total = Enemies.Count };
    }

    public void Update()
    {
        foreach (Enemy e in Enemies)
            e.Update();
        EnemyState.Value = EnemyState.Value with { alive = aliveCnt };
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
    
    // 적 팝업
    public void PopUpEnemy()
    {
        // 맵 위에 팝업 가능한 최대 개수를 넘지 말 것.
        if (CurEnemiesOnMap >= MaxEnemiesOnMap) return;
        // 적 최대 개수 이상 팝업되지 말 것.
        if (PopUpEnemyIndex >= totalCnt) return;
        
        Enemies[PopUpEnemyIndex++].PopUp();
        CurEnemiesOnMap++;
        aliveCnt++;
        EnemyState.Value = EnemyState.Value with { alive = aliveCnt };
    }
}