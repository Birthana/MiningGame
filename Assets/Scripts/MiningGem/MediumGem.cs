using UnityEngine;

public class MediumGem : MiningGem
{
    public MediumGem(Vector3Int position)
    {
        size_ = Size.MEDIUM;
        gemPosition = position;
        gemSprites = Resources.LoadAll<Sprite>("MediumGem");
    }

    public override bool IsOutOfBounds(int col, int row)
    {
        return gemPosition.x == col - 1 || gemPosition.y == row - 1;
    }

    public override Sprite[] GetRandomGemSprites()
    {
        int rngGem = Random.Range(0, gemSprites.Length / (int)size_) * (int)size_;
        return new Sprite[]{
                gemSprites[rngGem],
                gemSprites[rngGem + 1],
                gemSprites[rngGem + 2],
                gemSprites[rngGem + 3]
            };
    }

    public override Vector3Int[] GetGemPositions()
    {
        return new Vector3Int[] {
            gemPosition + new Vector3Int(0, 1, 0),
            gemPosition + new Vector3Int(1, 1, 0),
            gemPosition,
            gemPosition + new Vector3Int(1, 0, 0)
        };
    }
}

