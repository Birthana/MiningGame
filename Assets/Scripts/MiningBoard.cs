using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningBoard : MonoBehaviour
{
    [SerializeField] private Tilemap gemMap;
    [SerializeField] private int NUMBER_OF_COLUMNS;
    [SerializeField] private int NUMBER_OF_ROWS;

    #region Function: Getters & Setters
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

    public Tile GetBoardPosition(Vector3Int position)
    {
        return (Tile)gemMap.GetTile(position);
    }

    public void SetBoardPosition(Sprite sprite, Vector3Int position)
    {
        Tile newTile = ScriptableObject.CreateInstance<Tile>();
        newTile.sprite = sprite;
        gemMap.SetTile(position, newTile);
    }
    #endregion

    #region Function: Boolean
    public bool IsBoardPositionEmpty(Tile tile)
    {
        return tile == null;
    }
    public bool IsBoardPositionsEmpty(Vector3Int[] gemPositions)
    {
        foreach (Vector3Int position in gemPositions)
        {
            Tile rngTile = GetBoardPosition(position);
            if (!IsBoardPositionEmpty(rngTile))
                return false;
        }
        return true;
    }

    public bool IsOutOfBounds(MiningGem gem)
    {
        return gem.IsOutOfBounds(GetColumnCount(), GetRowCount());
    }
    #endregion
}
