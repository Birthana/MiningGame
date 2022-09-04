using UnityEngine;
public class SmallGem : MiningGem
{
    public SmallGem(Vector3Int position)
    {
        size_ = Size.SMALL;
        gemPosition = position;
        gemSprites = Resources.LoadAll<Sprite>("SmallGem");
    }

    public override Sprite[] GetRandomGemSprites()
    {
        int rngGem = Random.Range(0, gemSprites.Length / (int)size_) * (int)size_;
        return new Sprite[]{
                gemSprites[rngGem]
            };
    }

    public override Vector3Int[] GetGemPositions()
    {
        return new Vector3Int[] { gemPosition };
    }
}

