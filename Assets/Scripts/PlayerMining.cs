using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Tool tool;
    [SerializeField] private Wall wall;
    private bool gameEnd;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnd)
        {
            Mine();
        }
    }

    public void Mine()
    {
        Vector3Int tilePosition = ScreenToTilePosition(Input.mousePosition);
        if (wall.HasWall(tilePosition))
        {
            wall.RemoveTile(tilePosition);
            MineTiles(tool.GetAvailablePositions(tilePosition), 
                tool.GetExtraAvailablePositions(tilePosition));
            wall.Damage(tool.GetDamage());
        }
    }

    public Vector3Int ScreenToTilePosition(Vector3 clickedPosition)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(clickedPosition);
        Vector3Int tilePosition = wall.WorldToTilePosition(worldPosition);
        return tilePosition;
    }

    public void MineTiles(Vector3Int[] availablePositions, Vector3Int[] extraPositions)
    {
        int maxExtraTiles = tool.GetMaxExtraTiles();
        int currentExtraTiles = MineRandomTiles(tool.GetTilesToDestroy(), maxExtraTiles, availablePositions);
        MineRandomTiles(tool.GetExtraTilesToDestroyed(), currentExtraTiles, extraPositions);
    }

    private List<int> GetRandomIndexes(int numberOfRandomIndexes, int max)
    {
        List<int> rngIndexes = new List<int>();
        int rngNumber;
        for (int i = 0; i < numberOfRandomIndexes; i++)
        {
            do
            {
                rngNumber = Random.Range(0, max);
            } while (rngIndexes.Contains(rngNumber));
            rngIndexes.Add(rngNumber);
        }
        return rngIndexes;
    }

    private int MineRandomTiles(int tilesToMine, int maxExtraTiles, Vector3Int[] availableTiles)
    {
        List<int> rngTiles = GetRandomIndexes(tilesToMine, availableTiles.Length);
        int remainingExtraTiles = maxExtraTiles;
        for (int i = 0; i < availableTiles.Length; i++)
        {
            if (rngTiles.Contains(i))
            {
                wall.RemoveTile(availableTiles[i]);
                continue;
            }
            if (remainingExtraTiles == 0)
                continue;
            if (wall.ExtraRemoveTile(availableTiles[i], tool.GetExtraTilesPercentage()))
                remainingExtraTiles--;
        }
        return remainingExtraTiles;
    }
}
