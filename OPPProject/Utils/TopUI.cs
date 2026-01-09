public class TopUI
{
    private char _emptyHpSymbol = '♡';
    private char _fillHpSymbol = '♥';
    private ConsoleColor color = ConsoleColor.Red;
    
    public void Render(Hp health)
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < health.Total; i++)
        {
            if (i <= health.Current)
                _fillHpSymbol.Print(color);    
            else 
                _emptyHpSymbol.Print(color);
        }
    }
}