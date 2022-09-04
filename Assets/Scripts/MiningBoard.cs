using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningBoard : MonoBehaviour
{
    [SerializeField] private Tilemap gemMap;
    [SerializeField] private int NUMBER_OF_COLUMNS;
    [SerializeField] private int NUMBER_OF_ROWS;

    public int GetColumnCount()
    {
        return NUMBER_OF_COLUMNS;
    }

    public int GetRowCount()
    {
        return NUMBER_OF_ROWS;
    }

    public Vector3Int GetRandomBoardPosition()
    {
        return new Vector3Int(
            Random.Range(0, GetColumnCount()),
            Random.Range(0, GetRowCount()),
            0);
    }

    public bool IsBoardPositionEmpty(Tile tile)
    {
        return tile == null;
    }

    public bool IsOutOfBounds(MiningGem.Size size, Vector3Int position)
    {
        if (size == MiningGem.Size.LARGE)       
            return position.x == GetColumnCount() - 1 ||
                position.x == GetColumnCount() - 2 ||
                position.y == GetRowCount() - 1 ||
                position.y == GetRowCount() - 2;
        if (size == MiningGem.Size.MEDIUM)
            return position.x == GetColumnCount() - 1 || position.y == GetRowCount() - 1;
        return false;
    }

    public Tile GetBoardPosition(Vector3Int position)
    {
        return (Tile)gemMap.GetTile(position);
    }

    public void SetTileWithSpriteAtPosition(Sprite sprite, Vector3Int position)
    {
        Tile newTile = ScriptableObject.CreateInstance<Tile>();
        newTile.sprite = sprite;
        gemMap.SetTile(position, newTile);
    }

    public bool IsBoardPositionsEmpty(Vector3Int[] gemPositions)
    {
        for (int i = 0; i < gemPositions.Length; i++)
        {
            Tile rngTile = GetBoardPosition(gemPositions[i]);
            if (!IsBoardPositionEmpty(rngTile))
                return false;
        }
        return true;
    }

}
