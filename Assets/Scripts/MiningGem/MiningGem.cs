using UnityEngine;

public abstract class MiningGem
{
    public enum Size { SMALL = 1, MEDIUM = 4 , LARGE = 9 };

    protected Size size_;
    protected Vector3Int gemPosition;
    protected Sprite[] gemSprites;

    public static MiningGem GetRandomGem(float smallPercent, float mediumPercent,
        float LargePercent, Vector3Int rngGemPosition)
    {
        float rngNumber = Random.value;
        if (rngNumber < LargePercent)
            return new LargeGem(rngGemPosition);
        if (rngNumber - LargePercent < mediumPercent)
            return new MediumGem(rngGemPosition);
        return new SmallGem(rngGemPosition);
    }

    public Size GetSize()
    {
        return size_;
    }

    public Vector3Int GetGemPosition()
    {
        return gemPosition;
    }

    public abstract bool IsOutOfBounds(int col, int row);

    public abstract Sprite[] GetRandomGemSprites();

    public abstract Vector3Int[] GetGemPositions();
}
