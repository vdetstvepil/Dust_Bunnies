using UnityEngine;

public class SpawnSystemController : MonoBehaviour
{
    private SpawnPoint[] spawnPoints;

    void Start()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new SpawnPoint[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<SpawnPoint>();
            spawnPoints[i].Spawn();
        }
    }
}
