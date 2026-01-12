public class TopUI
{
    private char _emptyHpSymbol = '♡';
    private char _fillHpSymbol = '♥';
    private ConsoleColor _playerColor = ConsoleColor.Red;
    private ConsoleColor _enemyColor = ConsoleColor.Magenta;
    private int _rightAlignPos;
    
    public TopUI(int renderWidth) => _rightAlignPos = renderWidth - 1;

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
    public void RemainEnemy((int alive, int total) enemyCount)
    {
        string stateString = $"{enemyCount.alive} / {enemyCount.total}"; 
        Console.SetCursorPosition(_rightAlignPos - stateString.Length, 1);
        stateString.Print(_enemyColor);
    }
}