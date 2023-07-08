using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        int spawnPointCount = spawnPoints.Length;
        int npcCount = spawnPointCount;

        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPointCount; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < npcCount; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[randomIndex];
            Transform spawnPoint = spawnPoints[spawnIndex];

            Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);

            availableIndices.RemoveAt(randomIndex);
        }
    }
}
