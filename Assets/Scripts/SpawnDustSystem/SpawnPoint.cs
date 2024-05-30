using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] dustPrefabs;
    public float spawnHeight = 3f;

    public void Spawn()
    {
        if (dustPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, dustPrefabs.Length);
            GameObject dustPrefab = dustPrefabs[randomIndex];
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;
            GameObject spawnedDustPrefab = Instantiate(dustPrefab, spawnPosition, transform.rotation);
        }
    }

    public void SpawnAndDie()
    {
        if (dustPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, dustPrefabs.Length);
            GameObject dustPrefab = dustPrefabs[randomIndex];
            Vector3 spawnPosition = transform.position + Vector3.up * spawnHeight;
            GameObject spawnedDustPrefab = Instantiate(dustPrefab, spawnPosition, transform.rotation);
            Destroy(spawnedDustPrefab, 20f);
        }
    }
}
