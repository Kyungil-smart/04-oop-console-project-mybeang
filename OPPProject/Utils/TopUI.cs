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
    public void EnemyHpRender(Hp health)
    {
        // Enemy Side
        Console.SetCursorPosition(_rightAlignPos, 1);
        for (int i = _rightAlignPos; i > _rightAlignPos - health.Total; i--)
        {
            if (_rightAlignPos - health.Total <= health.Current)
                _fillHpSymbol.Print(_enemyColor);    
            else 
                _emptyHpSymbol.Print(_enemyColor);
        }
    }
}