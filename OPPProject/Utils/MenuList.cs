public class MenuList
{
    private List<(string text, Action action)> _menus;
    private int _currentIndex;
    private Ractangle _outline;
    private int _maxLength;
    public int CurrentIndex { get { return _currentIndex; } }

    public MenuList(params (string, Action)[] menuTexts)
    {
        if (menuTexts.Length == 0)
        {
            _menus = new List<(string, Action)>();
        }
        else
        {
            _menus = menuTexts.ToList();
        }

        for (int i = 0; i < _menus.Count; i++)
        {
            int textWidth = _menus[i].text.GetTextWidth();
            
            if (_maxLength < textWidth)
            {
                _maxLength = textWidth;
            }
        }

        _outline = new Ractangle(width: _maxLength + 4, height: _menus.Count + 2);
    }

    public void Reset()
    {
        _currentIndex = 0;
    }

    public void Select()
    {
        if (_menus.Count == 0) return;
        _menus[_currentIndex].action?.Invoke();
        
        if (_menus.Count == 0) _currentIndex = 0;
        else if (CurrentIndex >= _menus.Count) _currentIndex = _menus.Count - 1;
    }

    public void Add(string text, Action action)
    {
        _menus.Add((text, action));
        
        int textWidth = text.GetTextWidth();
        if (_maxLength < textWidth)
        {
            _maxLength = textWidth;
        }

        _outline.Width = _maxLength + 6;
        _outline.Height++;
    }

    public void RemoveAll()
    {
        _menus = new List<(string text, Action action)>();
        _currentIndex = 0;
    }

    public void CursorUp()
    {
        _currentIndex--;
        if (_currentIndex < 0) 
            _currentIndex = 0;
    }

    public void CursorDown()
    {
        _currentIndex++;
        if(_currentIndex >= _menus.Count) 
            _currentIndex = _menus.Count - 1;
    }

    public void Render(Vector2 position)
    {
        Render(position.X, position.Y);
    }
    
    public void Render(int x, int y)
    {
        _outline.X = x;
        _outline.Y = y;
        _outline.Draw();
        
        for(int i = 0; i < _menus.Count; i++)
        {
            y++;
            Console.SetCursorPosition(x + 1, y);
            
            if (i == _currentIndex)
            {
                "▶ ".Print(ConsoleColor.Green);
                _menus[i].text.Print(ConsoleColor.Green);
            }
            else
            {
                Console.Write("  ");
                _menus[i].text.Print();
            }
        }
    }
}