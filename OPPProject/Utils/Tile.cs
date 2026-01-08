public struct Tile
{
    public GameObject OnTileObject;
    public Vector2 Position;
    public Action PlayerOnAction; // 이벤트 발생
    public bool HasObject => OnTileObject != null;

    public Tile(GameObject onTileObject, Vector2 position)
    {
        OnTileObject = onTileObject;
        Position = position;
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