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

    private const int MEDIUM_GEM_TILES = 4;
    private const int LARGE_GEM_TILES = 9;
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

            rngGemPosition = GetRandomBoardPosition();

            if (randomGemSize == Gem.Size.SMALL)
                spawnedGem = SpawnSmallGem();
            else if (randomGemSize == Gem.Size.MEDIUM)
                spawnedGem = SpawnMediumGem();
            else if (randomGemSize == Gem.Size.LARGE)
                spawnedGem = SpawnLargeGem();

            if (spawnedGem != null)
                retryForEmptyBoardPosition = false;
        }
        return spawnedGem;
    }
    #endregion

    #region Function: Boolean
    private bool IsSmallGemAbleToSpawnAt()
    {
        return miningBoard.IsBoardPositionEmpty(miningBoard.GetBoardPosition(rngGemPosition));
    }

    private bool IsMediumGemAbleToSpawnAt()
    {
        return !IsMediumGemOutOfBounds(rngGemPosition) &&
            miningBoard.IsBoardPositionsEmpty(GetMediumGemPositions(rngGemPosition));
    }

    private bool IsMediumGemOutOfBounds(Vector3Int position)
    {
        return position.x == miningBoard.GetColumnCount() - 1 || position.y == miningBoard.GetRowCount() - 1;
    }

    private bool IsLargeGemAbleToSpawnAt()
    {
        return !IsLargeGemOutOfBounds(rngGemPosition) &&
            miningBoard.IsBoardPositionsEmpty(GetLargeGemPositions(rngGemPosition));
    }

    private bool IsLargeGemOutOfBounds(Vector3Int position)
    {
        return position.x == miningBoard.GetColumnCount() - 1 || 
            position.x == miningBoard.GetColumnCount() - 2 || 
            position.y == miningBoard.GetRowCount() - 1 || 
            position.y == miningBoard.GetRowCount() - 2;
    }
    #endregion

    #region Function: Gem Spawning
    private Vector3Int GetRandomBoardPosition()
    {
        return new Vector3Int(
            Random.Range(0, miningBoard.GetColumnCount()),
            Random.Range(0, miningBoard.GetRowCount()),
            0);
    }

    private Gem SpawnSmallGem()
    {
        if (!IsSmallGemAbleToSpawnAt())
            return null;
        return SpawnSmallGemAt();
    }

    private Gem SpawnSmallGemAt()
    {
        int rngSmallGem = Random.Range(0, smallGemSprites.Length);
        miningBoard.SetTileWithSpriteAtPosition(smallGemSprites[rngSmallGem], rngGemPosition);
        return new Gem(Gem.Size.SMALL, rngGemPosition);
    }

    private Gem SpawnMediumGem()
    {
        if (!IsMediumGemAbleToSpawnAt())
            return null;
        return SpawnMediumGemAt();
    }

    private Gem SpawnMediumGemAt()
    {
        int rngMediumGem = Random.Range(0, mediumGemSprites.Length / MEDIUM_GEM_TILES);

        Vector3Int[] gemPositions = GetMediumGemPositions(rngGemPosition);

        for (int i = 0; i < gemPositions.Length; i++)
        {
            miningBoard.SetTileWithSpriteAtPosition(mediumGemSprites[rngMediumGem * MEDIUM_GEM_TILES + i], 
                gemPositions[i]);
        }

        return new Gem(Gem.Size.MEDIUM, rngGemPosition);
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

    private Gem SpawnLargeGem()
    {
        if (!IsLargeGemAbleToSpawnAt())
            return null;
        return SpawnLargeGemAt();
    }

    private Gem SpawnLargeGemAt()
    {
        int rngLargeGem = Random.Range(0, largeGemSprites.Length / LARGE_GEM_TILES);

        Vector3Int[] gemPositions = GetLargeGemPositions(rngGemPosition);

        for (int i = 0; i < gemPositions.Length; i++)
        {
            miningBoard.SetTileWithSpriteAtPosition(largeGemSprites[rngLargeGem * LARGE_GEM_TILES + i],
                gemPositions[i]);
        }

        return new Gem(Gem.Size.LARGE, rngGemPosition);
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
