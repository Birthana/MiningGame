using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WoodHammer", menuName ="Tool/Hammer/Wood")]
public class WoodHammer : Tool
{
    public override Vector3Int[] GetAvailablePositions(Vector3Int position)
    {
        return  new Vector3Int[] {
            position + new Vector3Int(1, 0, 0),
            position + new Vector3Int(0, 1, 0),
            position + new Vector3Int(-1, 0, 0),
            position + new Vector3Int(0, -1, 0),
        };
    }

    public override Vector3Int[] GetExtraAvailablePositions(Vector3Int position)
    {
        return new Vector3Int[] {
            position + new Vector3Int(2, 0, 0),
            position + new Vector3Int(0, 2, 0),
            position + new Vector3Int(-2, 0, 0),
            position + new Vector3Int(0, -2, 0),
            position + new Vector3Int(1, -1, 0),
            position + new Vector3Int(1, 1, 0),
            position + new Vector3Int(-1, 1, 0),
            position + new Vector3Int(-1, -1, 0),
        };
    }
}
