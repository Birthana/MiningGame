using UnityEngine;

public class StartTest : MonoBehaviour
{
    private void Start()
    {
        GemSpawner test = FindObjectOfType<GemSpawner>();
        MiningGem[] gems = test.SpawnGems();
    }
}
