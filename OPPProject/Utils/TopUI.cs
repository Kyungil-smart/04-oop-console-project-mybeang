public enum TopUiAlign
{
    Left, Right, Center
}

public class TopUI
{
    private char _emptyHpSymbol = '♡';
    private char _fillHpSymbol = '♥';
    private ConsoleColor _playerColor = ConsoleColor.Red;
    private ConsoleColor _enemyColor = ConsoleColor.Magenta;
    private int _rightAlignPos;
    
    public TopUI(int renderWidth) => _rightAlignPos = renderWidth - 1;

    // 플레이어 HP 랜더링
    public void PlayerHpRender(Hp health)
    {
        // Player Side
        Console.SetCursorPosition(1, 1);
        for (int i = 0; i < health.Total; i++)
        {
            if (i <= health.Current)
                _fillHpSymbol.Print(_playerColor);
            else
                _emptyHpSymbol.Print(_playerColor);
        }
    }
    // 남은 적 수 / 전체 적 수 랜더링
    public void RemainEnemy((int alive, int total) enemyCount)
    {
        string stateString = $"{enemyCount.alive} / {enemyCount.total}"; 
        Console.SetCursorPosition(_rightAlignPos - stateString.Length, 1);
        stateString.Print(_enemyColor);
    }
    
    // Stage 확인
    public void ViewCurrentStage(TopUiAlign align, int left, int top)
    {
        string curStage = $"Stage {SceneManager.StageNumber}";
        if (align == TopUiAlign.Center) 
            left -= curStage.Length / 2;
        else if (align == TopUiAlign.Right)
            left -= curStage.Length;
            
        Console.SetCursorPosition(left, top);
        curStage.Print(ConsoleColor.Cyan);
    }
}