using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTest : MonoBehaviour
{
    private void Start()
    {
        GemSpawner test = FindObjectOfType<GemSpawner>();
        Gem[] gems = test.SpawnGems();
    }
}
