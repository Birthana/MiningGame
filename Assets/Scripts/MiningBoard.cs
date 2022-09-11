using UnityEngine;
using UnityEngine.Tilemaps;

public class MiningBoard : MonoBehaviour
{
    [SerializeField] private Tilemap board;
    [SerializeField] private int NUMBER_OF_COLUMNS;
    [SerializeField] private int NUMBER_OF_ROWS;

    #region Function: Getters & Setters
    public Vector3Int WorldToTilePosition(Vector3 worldPosition)
    {
        return board.WorldToCell(worldPosition);
    }

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
        return (Tile)board.GetTile(position);
    }

    public void SetBoardPosition(Sprite sprite, Vector3Int position)
    {
        Tile newTile = ScriptableObject.CreateInstance<Tile>();
        newTile.sprite = sprite;
        board.SetTile(position, newTile);
    }

    public void SetBoardPositionToEmpty(Vector3Int position)
    {
        board.SetTile(position, null);
    }
    #endregion

    #region Function: Boolean
    public bool IsBoardPositionEmpty(Vector3Int position)
    {
        return !board.HasTile(position);
    }

    public bool IsBoardPositionsEmpty(Vector3Int[] positions)
    {
        foreach (Vector3Int position in positions)
        {
            if (!IsBoardPositionEmpty(position))
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
