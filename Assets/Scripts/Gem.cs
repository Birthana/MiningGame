using UnityEngine;

public class Gem
{
    public enum Size { SMALL = 1 , MEDIUM = 4, LARGE = 9 };
    private Size size_;
    private Vector3Int gemPosition;

    public static Size GetRandomWeightedSize(float smallPercent, float mediumPercent, float LargePercent)
    {
        float rngNumber = Random.value;
        if (rngNumber < LargePercent)
        {
            return Size.LARGE;
        }
        else if (rngNumber - LargePercent < mediumPercent)
        {
            return Size.MEDIUM;
        }
        else
        {
            return Size.SMALL;
        }
    }

    #region Function: Constructors
    public Gem(Size newSize, Vector3Int newGemPosition)
    {
        size_ = newSize;
        gemPosition = newGemPosition;
    }
    #endregion

    #region Function: Setters & Getters
    public Vector3Int getGemPosition()
    {
        return gemPosition;
    }

    public Size GetSize()
    {
        return size_;
    }
    #endregion
}
