using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Tool tool;
    [SerializeField] private Wall wall;

    private int remainingExtraTiles = 0;
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

    public Vector3Int ScreenToTilePosition(Vector3 clickedPosition)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(clickedPosition);
        Vector3Int tilePosition = wall.WorldToTilePosition(worldPosition);
        return tilePosition;
    }

    public void Mine()
    {
        Vector3Int selectedPosition = ScreenToTilePosition(Input.mousePosition);
        if (wall.HasWall(selectedPosition))
        {
            remainingExtraTiles = tool.GetMaxExtraTiles();
            wall.RemoveTile(selectedPosition);
            MineGuaranteeTilesAround(selectedPosition);
            MineExtraTilesAround(selectedPosition);
            wall.Damage(tool.GetDamage());
        }
    }

    public void MineGuaranteeTilesAround(Vector3Int selectedPosition)
    {
        MineSurroundingTiles(tool.GetTilesToDestroy(), 
            tool.GetAvailablePositions(selectedPosition));
    }

    public void MineExtraTilesAround(Vector3Int selectedPosition)
    {
        MineSurroundingTiles(tool.GetExtraTilesToDestroy(), 
            tool.GetExtraAvailablePositions(selectedPosition));
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

    private void MineSurroundingTiles(int tilesToMine, Vector3Int[] surroundingTiles)
    {
        List<int> rngTiles = GetRandomIndexes(tilesToMine, surroundingTiles.Length);
        for (int i = 0; i < surroundingTiles.Length; i++)
        {
            if (rngTiles.Contains(i))
            {
                wall.RemoveTile(surroundingTiles[i]);
                continue;
            }
            ChanceToMineTiles(surroundingTiles[i]);
        }
    }

    private void ChanceToMineTiles(Vector3Int position)
    {
        if (remainingExtraTiles == 0)
            return;
        if (wall.ExtraRemoveTile(position, tool.GetExtraTilesPercentage()))
            remainingExtraTiles--;
    }
}
