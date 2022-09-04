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
    public MiningGem[] SpawnGems()
    {
        //TODO: If first gem, spawn a medium or large gem.
        MiningGem[] spawnedGems = new MiningGem[GetNumberOfGemsToSpawnFromBoardSize()];
        for (int i = 0; i < spawnedGems.Length; i++)
        {
            spawnedGems[i] = SpawnRandomSizedGem();
        }
        return spawnedGems;
    }

    private int GetNumberOfGemsToSpawnFromBoardSize()
    {
        float numberOfGemsToSpawn = miningBoard.GetColumnCount() * miningBoard.GetRowCount() * GEM_SPAWN_PERCENT;
        float actualVariance = Random.Range(-VARIENCE, VARIENCE);
        return Mathf.FloorToInt(numberOfGemsToSpawn + actualVariance);
    }

    private MiningGem SpawnRandomSizedGem()
    {
        MiningGem rngGem = null;
        bool retryForEmptyBoardPosition = true;
        while (retryForEmptyBoardPosition)
        {
            rngGem = MiningGem.GetRandomGem(
                SMALL_GEM_SPAWN,
                MEDIUM_GEM_SPAWN,
                LARGE_GEM_SPAWN,
                miningBoard.GetRandomBoardPosition());

            if (IsGemAbleToSpawnAt(rngGem))
            {
                SpawnGemAt(rngGem);
                retryForEmptyBoardPosition = false;
            }
        }
        return rngGem;
    }
    #endregion

    #region Function: Boolean

    private bool IsGemAbleToSpawnAt(MiningGem gem)
    {
        return !miningBoard.IsOutOfBounds(gem.GetSize(), gem.GetGemPosition()) &&
            miningBoard.IsBoardPositionsEmpty(gem.GetGemPositions());
    }
    #endregion

    #region Function: Gem Spawning

    private void SpawnGemAt(MiningGem gem)
    {
        Sprite[] gemSprites = gem.GetRandomGemSprites();
        Vector3Int[] gemPositions = gem.GetGemPositions();

        for (int i = 0; i < gemPositions.Length; i++)
        {
            miningBoard.SetTileWithSpriteAtPosition(gemSprites[i], gemPositions[i]);
        }
    }
    #endregion
}
