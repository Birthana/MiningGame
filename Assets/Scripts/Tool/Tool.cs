using UnityEngine;

public abstract class Tool : ScriptableObject
{
    [SerializeField] private Sprite icon;

    [Header("Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int tilesToDestroy;
    [SerializeField] private int extraTilesToDestroy;
    [Range(0, 1)]
    [SerializeField] private float extraTilesPercentage;
    [SerializeField] private int maxExtraTiles;

    public int GetDamage() { return damage; }

    public int GetTilesToDestroy() { return tilesToDestroy; }

    public int GetExtraTilesToDestroy() { return extraTilesToDestroy; }

    public float GetExtraTilesPercentage() { return extraTilesPercentage; }

    public int GetMaxExtraTiles() { return maxExtraTiles; }

    public abstract Vector3Int[] GetAvailablePositions(Vector3Int position);

    public abstract Vector3Int[] GetExtraAvailablePositions(Vector3Int position);
}
