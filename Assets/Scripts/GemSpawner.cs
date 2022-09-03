using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    #region Field: Mining Board
    [Header("Mining Board")]
    [SerializeField] private MiningBoard miningBoard;
    #endregion

    #region Field: Gem Percentage 
    [Header("Gem Percentages")]
    [Range(0, 1f)]
    [SerializeField] private float GEM_SPAWN_PERCENT;
    [Range(0, 5)]
    [SerializeField] private float VARIENCE;
    [Range(0, .5f)]
    [SerializeField] private float LARGE_GEM_SPAWN;
    [Range(0, .5f)]
    [SerializeField] private float MEDIUM_GEM_SPAWN;
    private float SMALL_GEM_SPAWN;
    #endregion

    #region Field: Gem Sprites
    [Header("Sprites")]

    [SerializeField] private Sprite[] smallGemSprites;
    [SerializeField] private Sprite[] mediumGemSprites;
    [SerializeField] private Sprite[] largeGemSprites;
    #endregion

    private Vector3Int rngGemPosition;

    #region Function: Setup
    private void Start()
    {
        CalculateSmallGemSpawnPercentage();
    }

    private void CalculateSmallGemSpawnPercentage()
    {
        SMALL_GEM_SPAWN = 100 - (LARGE_GEM_SPAWN + MEDIUM_GEM_SPAWN);
    }
    #endregion

    #region Function: Main
    public Gem[] SpawnGems()
    {
        //TODO: If first gem, spawn a medium or large gem.
        Gem[] spawnedGems = new Gem[GetNumberOfGemsToSpawnFromBoardSize()];
        for (int i = 0; i < spawnedGems.Length; i++)
        {
            spawnedGems[i] = SpawnRandomSizedGem();
        }
        return spawnedGems;
    }

    private int GetNumberOfGemsToSpawnFromBoardSize()
    {
        float numberOfGemsToSpawn = miningBoard.GetColumnCount() * miningBoard.GetRowCount() * GEM_SPAWN_PERCENT;
        float actualVariance = Random.Range(0, VARIENCE * 2 + 1) - VARIENCE;
        return Mathf.FloorToInt(numberOfGemsToSpawn + actualVariance);
    }

    private Gem SpawnRandomSizedGem()
    {
        Gem spawnedGem = null;
        bool retryForEmptyBoardPosition = true;
        while (retryForEmptyBoardPosition)
        {
            Gem.Size randomGemSize = Gem.GetRandomWeightedSize(
                SMALL_GEM_SPAWN,
                MEDIUM_GEM_SPAWN,
                LARGE_GEM_SPAWN);

            rngGemPosition = miningBoard.GetRandomBoardPosition();

            if (randomGemSize == Gem.Size.SMALL && IsSmallGemAbleToSpawnAt())
                spawnedGem = SpawnSmallGemAt();
            else if (randomGemSize == Gem.Size.MEDIUM && IsMediumGemAbleToSpawnAt())
                spawnedGem = SpawnGemAt(Gem.Size.MEDIUM, GetMediumGemPositions(rngGemPosition));
            else if (randomGemSize == Gem.Size.LARGE && IsLargeGemAbleToSpawnAt())
                spawnedGem = SpawnGemAt(Gem.Size.LARGE, GetLargeGemPositions(rngGemPosition));

            if (spawnedGem != null)
                retryForEmptyBoardPosition = false;
        }
        return spawnedGem;
    }
    #endregion

    #region Function: Boolean
    private bool IsSmallGemAbleToSpawnAt()
    {
        return !IsOutOfBounds(Gem.Size.SMALL);
    }

    private bool IsMediumGemAbleToSpawnAt()
    {
        return !IsOutOfBounds(Gem.Size.MEDIUM) &&
            miningBoard.IsBoardPositionsEmpty(GetMediumGemPositions(rngGemPosition));
    }

    private bool IsLargeGemAbleToSpawnAt()
    {
        return !IsOutOfBounds(Gem.Size.LARGE) &&
            miningBoard.IsBoardPositionsEmpty(GetLargeGemPositions(rngGemPosition));
    }

    private bool IsOutOfBounds(Gem.Size size)
    {
        if(size == Gem.Size.LARGE)
            return rngGemPosition.x == miningBoard.GetColumnCount() - 1 ||
                rngGemPosition.x == miningBoard.GetColumnCount() - 2 ||
                rngGemPosition.y == miningBoard.GetRowCount() - 1 ||
                rngGemPosition.y == miningBoard.GetRowCount() - 2;
        else if(size == Gem.Size.MEDIUM)
            return rngGemPosition.x == miningBoard.GetColumnCount() - 1 || rngGemPosition.y == miningBoard.GetRowCount() - 1;
        else
            return miningBoard.IsBoardPositionEmpty(miningBoard.GetBoardPosition(rngGemPosition));
    }
    #endregion

    #region Function: Gem Spawning

    private Gem SpawnSmallGemAt()
    {
        int rngSmallGem = Random.Range(0, smallGemSprites.Length);
        miningBoard.SetTileWithSpriteAtPosition(smallGemSprites[rngSmallGem], rngGemPosition);
        return new Gem(Gem.Size.SMALL, rngGemPosition);
    }

    private Gem SpawnGemAt(Gem.Size size, Vector3Int[] gemPositions)
    {
        int rngGem = GetRandomGemIndex(size);
        Sprite[] gemSprites = GetGemSprites(size);

        for (int i = 0; i < gemPositions.Length; i++)
        {
            miningBoard.SetTileWithSpriteAtPosition(gemSprites[rngGem + i],
                gemPositions[i]);
        }

        return new Gem(size, rngGemPosition);
    }

    private int GetRandomGemIndex(Gem.Size size)
    {
        int numberOfPossibleGems = 0;
        if (size == Gem.Size.SMALL)
            numberOfPossibleGems = smallGemSprites.Length;
        else if (size == Gem.Size.MEDIUM)
            numberOfPossibleGems = mediumGemSprites.Length;
        else if (size == Gem.Size.LARGE)
            numberOfPossibleGems = largeGemSprites.Length;

        return Random.Range(0, numberOfPossibleGems / (int)size) * (int)size;
    }

    private Sprite[] GetGemSprites(Gem.Size size)
    {
        if (size == Gem.Size.SMALL)
            return smallGemSprites;
        else if (size == Gem.Size.MEDIUM)
            return mediumGemSprites;
        else
            return largeGemSprites;
    }

    private Vector3Int[] GetMediumGemPositions(Vector3Int position)
    {
        return new Vector3Int[] {
            position + new Vector3Int(0, 1, 0),
            position + new Vector3Int(1, 1, 0),
            position,
            position + new Vector3Int(1, 0, 0)
        };
    }

    private Vector3Int[] GetLargeGemPositions(Vector3Int position)
    {
        return new Vector3Int[] {
            position + new Vector3Int(0, 2, 0),
            position + new Vector3Int(1, 2, 0),
            position + new Vector3Int(2, 2, 0),
            position + new Vector3Int(0, 1, 0),
            position + new Vector3Int(1, 1, 0),
            position + new Vector3Int(2, 1, 0),
            position,
            position + new Vector3Int(1, 0, 0),
            position + new Vector3Int(2, 0, 0),
        };
    }

    #endregion
}
