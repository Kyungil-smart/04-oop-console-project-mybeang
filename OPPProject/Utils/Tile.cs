public struct Tile
{
    public GameObject OnTileObject;
    private GameObject _backUpObject;
    public Vector2 Position;
    public Action PlayerOnAction; // 이벤트 발생
    public bool HasObject => OnTileObject != null;

    public Tile(GameObject onTileObject, Vector2 position)
    {
        OnTileObject = onTileObject;
        _backUpObject = onTileObject;
        if (HasObject) OnTileObject.Position = position;
        Position = position;
    }

    public void StepOn(GameObject steppingObject)
    {
        OnTileObject = steppingObject;
    }

    public void StepOff()
    {
        OnTileObject = _backUpObject;
    }

    public void Print()
    {
        if (HasObject)
        {
            OnTileObject.Symbol.Print(OnTileObject.Color);
        }
        else
        {
            ' '.Print();
        }
    }
}