using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenPlatformSpawner : MonoBehaviour
{
    public GameObject goldenPlatformPrefab;
    public Transform spawnPoint;
    public GameObject targetPoint;
    private GameObject spawnedPlatform;

    public void SpawnGoldenPlatform()
    {
        spawnedPlatform = Instantiate(goldenPlatformPrefab, spawnPoint.position, Quaternion.identity);
        GoldenPlatformMover platformMover = spawnedPlatform.GetComponent<GoldenPlatformMover>();

        if (platformMover != null && targetPoint != null)
        {
            platformMover.SetTarget(targetPoint);
        }
    }
}
