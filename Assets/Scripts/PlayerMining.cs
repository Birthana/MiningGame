using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{
    public Camera cam;
    public Wall wall;
    public bool gameEnd;

    private Vector3Int[] obsidian = new Vector3Int[] {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0)
    };

    private Vector3Int[] diamond = new Vector3Int[]
    {
        new Vector3Int(0, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(0, -1, 0)
    };

    private Vector3Int[] innerRing = new Vector3Int[]
    {
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 1, 0),
        new Vector3Int(1, -1, 0)
    };

    private Vector3Int[] outerRing = new Vector3Int[]
    {
        new Vector3Int(2, 0, 0),
        new Vector3Int(0, 2, 0),
        new Vector3Int(-2, 0, 0),
        new Vector3Int(0, -2, 0),
        new Vector3Int(2, 2, 0),
        new Vector3Int(-2, -2, 0),
        new Vector3Int(-2, 2, 0),
        new Vector3Int(2, -2, 0),
        new Vector3Int(1, 2, 0),
        new Vector3Int(2, 1, 0),
        new Vector3Int(-1, 2, 0),
        new Vector3Int(2, -1, 0),
        new Vector3Int(-2, 1, 0),
        new Vector3Int(-2, -1, 0),
        new Vector3Int(1, -2, 0),
        new Vector3Int(-1, -2, 0)
    };

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnd)
        {
            Mine();
        }
    }

    public void Mine()
    {
        string toolSpriteName = "tools_9";
        Vector3Int tilePosition = ScreenToTilePosition(Input.mousePosition);
        if (wall.HasWall(tilePosition))
        {
            switch (toolSpriteName)
            {
                case "tools_9":
                    //Wood Hammer
                    BaseTool(true, 2, 0, 5, 1);
                    break;
                case "tools_11":
                    //Copper Hammer
                    BaseTool(true, 3, 0, 10, 0);
                    break;
                case "tools_14":
                    //Bone Hammer
                    wall.RemoveTile(tilePosition);
                    BaseTool(true, 2, 0, 10, 0);
                    break;
                case "tools_12":
                    //Gold Hammer
                    BaseTool(true, 2, 0, 30, 20);
                    break;
                case "tools_10":
                    //Iron Hammer
                    BaseTool(true, 4, 3, 10, 0);
                    break;
                case "tools_16":
                    //Obsidian Hammer
                    foreach (Vector3Int position in obsidian)
                    {
                        wall.RemoveTile(tilePosition + position);
                    }
                    BaseTool(true, 2, 0, 10, 0);
                    break;
                case "tools_17":
                    //Magic Hammer
                    BaseTool(true, 2, 0, 45, 35);
                    break;
                case "tools_13":
                    //Steel Hammer
                    BaseTool(true, 5, 6, 10, 0);
                    break;
                case "tools_15":
                    //Diamond Hammer
                    foreach (Vector3Int position in diamond)
                    {
                        wall.RemoveTile(tilePosition + position);
                    }
                    BaseTool(true, 2, 0, 10, 0);
                    break;
                case "tools_0":
                    //Wood Pickaxe
                    BaseTool(false, 0, 0, 5, 5);
                    break;
                case "tools_2":
                    //Copper Pickaxe
                    BaseTool(false, 1, 0, 0, 0);
                    break;
                case "tools_5":
                    //Bone Pickaxe
                    BaseTool(false, 0, 0, 0, 0);
                    BaseTool(false, 0, 0, 0, 0);
                    break;
                case "tools_3":
                    //Gold Pickaxe
                    BaseTool(false, 0, 0, 10, 10);
                    break;
                case "tools_1":
                    //Iron Pickaxe
                    BaseTool(false, 2, 1, 0, 0);
                    break;
                case "tools_7":
                    //Obsidian Pickaxe
                    foreach (Vector3Int position in obsidian)
                    {
                        wall.RemoveTile(tilePosition + position);
                    }
                    BaseTool(false, 0, 0, 0, 0);
                    break;
                case "tools_8":
                    //Magic Pickaxe
                    BaseTool(false, 0, 0, 15, 15);
                    break;
                case "tools_4":
                    //Steel Pickaxe
                    BaseTool(false, 3, 2, 0, 0);
                    break;
                case "tools_6":
                    //Diamond Pickaxe
                    foreach (Vector3Int position in diamond)
                    {
                        wall.RemoveTile(tilePosition + position);
                    }
                    BaseTool(false, 0, 0, 0, 0);
                    break;
            }
        }

    }

    public Vector3Int ScreenToTilePosition(Vector3 clickedPosition)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(clickedPosition);
        Vector3Int tilePosition = wall.WorldToTilePosition(worldPosition);
        return tilePosition;
    }

    public void BaseTool(bool isHammer, int innerRingTiles, int outerRingTiles, 
        int extraInnerTilePercantage, int extraOuterTilePercantage)
    {
        Vector3Int tilePosition = ScreenToTilePosition(Input.mousePosition);
        wall.RemoveTile(tilePosition);

        MineRandomTiles(tilePosition, innerRingTiles, extraInnerTilePercantage, innerRing);
        MineRandomTiles(tilePosition, outerRingTiles, extraOuterTilePercantage, outerRing);

        if (isHammer)
            wall.Damage(3);
        else
            wall.Damage(1);

    }

    private List<int> GetRandomIndexes(int numberOfRandomNumbers, int max)
    {
        List<int> rngNumbers = new List<int>();
        int rngNumber;
        for (int i = 0; i < numberOfRandomNumbers; i++)
        {
            do
            {
                rngNumber = Random.Range(0, max);
            } while (rngNumbers.Contains(rngNumber));
            rngNumbers.Add(rngNumber);
        }
        return rngNumbers;
    }

    private void MineRandomTiles(Vector3Int tilePosition, int tilesToMine, int percentageToMine, Vector3Int[] availableTiles)
    {
        List<int> rngTiles = GetRandomIndexes(tilesToMine, availableTiles.Length);
        for (int i = 0; i < availableTiles.Length; i++)
        {
            if (rngTiles.Contains(i))
            {
                wall.RemoveTile(tilePosition + availableTiles[i]);
                continue;
            }
            wall.ExtraRemoveTile(tilePosition + availableTiles[i], percentageToMine);
        }
    }
}
