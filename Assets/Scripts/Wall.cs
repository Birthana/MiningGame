using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wall : MonoBehaviour
{
    public MiningBoard[] layers;
    public int maxHealth = 30;
    private int currentHealth = 0;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        currentHealth = maxHealth;
    }

    public Vector3Int WorldToTilePosition(Vector3 worldPosition)
    {
        return layers[0].WorldToTilePosition(worldPosition);
    }

    public bool HasWall(Vector3Int position)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (!layers[i].IsBoardPositionEmpty(position))
                return true;
        }
        return false;
    }

    public void RemoveTile(Vector3Int tilePosition)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            if (!layers[i].IsBoardPositionEmpty(tilePosition))
            {
                layers[i].SetBoardPositionToEmpty(tilePosition);
                break;
            }
        }
    }

    public void ExtraRemoveTile(Vector3Int position, int chance)
    {
        int extra = Random.Range(1, 100);
        if (extra <= chance)
            RemoveTile(position);
    }

    public void Damage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        // If health is 0, end game.
    }
}
