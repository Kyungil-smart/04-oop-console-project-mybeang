// 맵 기본 단위 객체
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

    // 타일 위에 동적으로 GameObject 가 있을 경우
    public void StepOn(GameObject steppingObject)
    {
        OnTileObject = steppingObject;
    }

    // 타일 위에서 동적으로 GameObject가 제거됬을 경우
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