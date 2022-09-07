using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{
    public Camera cam;
    public Tilemap[] tilemap;
    public GameObject selector;
    public int clicks = 0;
    public int max = 30;
    public bool gameEnd;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnd)
        {
            string toolSpriteName = selector.name;
            Vector3Int tilePosition = ScreenToTilePosition(Input.mousePosition);
            if (tilemap[0].GetTile(tilePosition) != null || tilemap[1].GetTile(tilePosition) != null)
            {
                switch (toolSpriteName)
                {
                    case "tools_9":
                        //Wood Hammer
                        BaseTool(true, 2, 0, 15, 5);
                        break;
                    case "tools_11":
                        //Copper Hammer
                        BaseTool(true, 3, 0, 10, 0);
                        break;
                    case "tools_14":
                        //Bone Hammer
                        RemoveTile(tilePosition);
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
                        RemoveTile(tilePosition);
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
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
                        RemoveTile(tilePosition);
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
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
                        RemoveTile(tilePosition);
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
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
                        RemoveTile(tilePosition);
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
                        RemoveTile(tilePosition + new Vector3Int(1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(-1, 0, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, 1, 0));
                        RemoveTile(tilePosition + new Vector3Int(0, -1, 0));
                        BaseTool(false, 0, 0, 0, 0);
                        break;
                }
            }
        }
    }

    public Vector3Int ScreenToTilePosition(Vector3 clickedPosition)
    {
        Vector3 worldPosition = cam.ScreenToWorldPoint(clickedPosition);
        Vector3Int tilePosition = tilemap[0].WorldToCell(worldPosition);
        return tilePosition;
    }

    public void BaseTool(bool isHammer, int surroundingAreaA, int surroundingAreaB, int innerPercentage, int outerPercentage)
    {
        Vector3Int tilePosition = ScreenToTilePosition(Input.mousePosition);
        RemoveTile(tilePosition);
        Vector3Int[] surroundingTilePositions = new Vector3Int[24];
        surroundingTilePositions[0] = tilePosition + new Vector3Int(1, 0, 0);
        surroundingTilePositions[1] = tilePosition + new Vector3Int(0, 1, 0);
        surroundingTilePositions[2] = tilePosition + new Vector3Int(-1, 0, 0);
        surroundingTilePositions[3] = tilePosition + new Vector3Int(0, -1, 0);
        surroundingTilePositions[4] = tilePosition + new Vector3Int(1, 1, 0);
        surroundingTilePositions[5] = tilePosition + new Vector3Int(-1, -1, 0);
        surroundingTilePositions[6] = tilePosition + new Vector3Int(-1, 1, 0);
        surroundingTilePositions[7] = tilePosition + new Vector3Int(1, -1, 0);

        surroundingTilePositions[8] = tilePosition + new Vector3Int(2, 0, 0);
        surroundingTilePositions[9] = tilePosition + new Vector3Int(0, 2, 0);
        surroundingTilePositions[10] = tilePosition + new Vector3Int(-2, 0, 0);
        surroundingTilePositions[11] = tilePosition + new Vector3Int(0, -2, 0);
        surroundingTilePositions[12] = tilePosition + new Vector3Int(2, 2, 0);
        surroundingTilePositions[13] = tilePosition + new Vector3Int(-2, -2, 0);
        surroundingTilePositions[14] = tilePosition + new Vector3Int(-2, 2, 0);
        surroundingTilePositions[15] = tilePosition + new Vector3Int(2, -2, 0);
        surroundingTilePositions[16] = tilePosition + new Vector3Int(1, 2, 0);
        surroundingTilePositions[17] = tilePosition + new Vector3Int(2, 1, 0);
        surroundingTilePositions[18] = tilePosition + new Vector3Int(-1, 2, 0);
        surroundingTilePositions[19] = tilePosition + new Vector3Int(2, -1, 0);
        surroundingTilePositions[20] = tilePosition + new Vector3Int(-2, 1, 0);
        surroundingTilePositions[21] = tilePosition + new Vector3Int(-2, -1, 0);
        surroundingTilePositions[22] = tilePosition + new Vector3Int(1, -2, 0);
        surroundingTilePositions[23] = tilePosition + new Vector3Int(-1, -2, 0);

        List<int> previousRNG = new List<int>();
        for (int i = 0; i < surroundingAreaA; i++)
        {
            bool looking = true;
            while (looking)
            {
                int rng = UnityEngine.Random.Range(0, 8);
                if (!previousRNG.Contains(rng))
                {
                    RemoveTile(surroundingTilePositions[rng]);
                    surroundingTilePositions[rng] = new Vector3Int(0, 0, -1);
                    previousRNG.Add(rng);
                    looking = false;
                }
            }
        }

        for (int i = 0; i < surroundingAreaB; i++)
        {
            bool looking = true;
            while (looking)
            {
                int rng = UnityEngine.Random.Range(0, 16);
                if (!previousRNG.Contains(rng + 8))
                {
                    RemoveTile(surroundingTilePositions[rng + 8]);
                    surroundingTilePositions[rng + 8] = new Vector3Int(0, 0, -1);
                    previousRNG.Add(rng + 8);
                    looking = false;
                }
            }
        }

        for (int i = 0; i < surroundingTilePositions.Length; i++)
        {
            if (!(surroundingTilePositions[i].Equals(new Vector3Int(0, 0, -1))))
            {
                if (i < 8)
                {
                    ExtraRemoveTile(surroundingTilePositions[i], innerPercentage);
                }
                else
                {
                    ExtraRemoveTile(surroundingTilePositions[i], outerPercentage);
                }
            }
        }

        if (isHammer)
        {
            clicks += 3;
            //clickCountRatio.text = (max - clicks) + " / " + max;
            //healthBar.GetComponent<MiningWallHealthBar>().SetPercentage(max - clicks, max, false);
        }
        else
        {
            clicks += 1;
            //clickCountRatio.text = (max - clicks) + " / " + max;
            //healthBar.GetComponent<MiningWallHealthBar>().SetPercentage(max - clicks, max, false);
        }

    }

    public void RemoveTile(Vector3Int tilePosition)
    {
        if (tilemap[0].HasTile(tilePosition))
        {
            tilemap[0].SetTile(tilePosition, null);
        }
        else if (tilemap[1].HasTile(tilePosition))
        {
            tilemap[1].SetTile(tilePosition, null);
        }
    }

    public void ExtraRemoveTile(Vector3Int position, int chance)
    {
        if (tilemap[0].HasTile(position)
                && tilemap[1].HasTile(position))
        {
            int extra = UnityEngine.Random.Range(1, 100);
            if (extra <= chance)
            {
                RemoveTile(position);
            }
        }
    }


}
