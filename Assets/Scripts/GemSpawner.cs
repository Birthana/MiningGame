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
        bool retryForValidGem = true;
        while (retryForValidGem)
        {
            MiningGem rngGem = MiningGem.GetRandomGem(
                SMALL_GEM_SPAWN,
                MEDIUM_GEM_SPAWN,
                LARGE_GEM_SPAWN,
                miningBoard.GetRandomBoardPosition());

            if (IsAbleToSpawn(rngGem))
            {
                SpawnInMiningBoard(rngGem);
                return rngGem;
            }
        }
        return null;
    }
    #endregion

    #region Function: Boolean

    private bool IsAbleToSpawn(MiningGem gem)
    {
        return !miningBoard.IsOutOfBounds(gem) &&
            miningBoard.IsBoardPositionsEmpty(gem.GetGemPositions());
    }
    #endregion

    #region Function: Gem Spawning

    private void SpawnInMiningBoard(MiningGem gem)
    {
        Sprite[] gemSprites = gem.GetRandomGemSprites();
        Vector3Int[] gemPositions = gem.GetGemPositions();

        for (int i = 0; i < gemPositions.Length; i++)
        {
            miningBoard.SetBoardPosition(gemSprites[i], gemPositions[i]);
        }
    }
    #endregion
}
