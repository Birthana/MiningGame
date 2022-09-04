using UnityEngine;

public class LargeGem : MiningGem
{
    public LargeGem(Vector3Int position)
    {
        size_ = Size.LARGE;
        gemPosition = position;
        gemSprites = Resources.LoadAll<Sprite>("LargeGem");
    }

    public override Sprite[] GetRandomGemSprites()
    {
        int rngGem = Random.Range(0, gemSprites.Length / (int)size_) * (int)size_;
        return new Sprite[]{
                gemSprites[rngGem],
                gemSprites[rngGem + 1],
                gemSprites[rngGem + 2],
                gemSprites[rngGem + 3],
                gemSprites[rngGem + 4],
                gemSprites[rngGem + 5],
                gemSprites[rngGem + 6],
                gemSprites[rngGem + 7],
                gemSprites[rngGem + 8],
            };
    }

    public override Vector3Int[] GetGemPositions()
    {
        return new Vector3Int[] {
            gemPosition + new Vector3Int(0, 2, 0),
            gemPosition + new Vector3Int(1, 2, 0),
            gemPosition + new Vector3Int(2, 2, 0),
            gemPosition + new Vector3Int(0, 1, 0),
            gemPosition + new Vector3Int(1, 1, 0),
            gemPosition + new Vector3Int(2, 1, 0),
            gemPosition,
            gemPosition + new Vector3Int(1, 0, 0),
            gemPosition + new Vector3Int(2, 0, 0),
        };
    }
}
