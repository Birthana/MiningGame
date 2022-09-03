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
        Tile[] rngTiles = new Tile[gemPositions.Length];
        for (int i = 0; i < gemPositions.Length; i++)
        {
            rngTiles[i] = GetBoardPosition(gemPositions[i]);
            if (!IsBoardPositionEmpty(rngTiles[i]))
                return false;
        }
        return true;
    }

}
