using UnityEngine;

public class SpawnAndDieSystemController : MonoBehaviour
{
    public float spawnInterval = 10f;

    private SpawnPoint[] spawnPoints;
    private float timer;

    void Start()
    {
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        spawnPoints = new SpawnPoint[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<SpawnPoint>();
            Debug.Log("SpawnPoint: " + spawnPoints[i].name);
        }

        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnAtRandomPoint();
            timer = spawnInterval;
        }
    }

    void SpawnAtRandomPoint()
    {
        if (spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            spawnPoints[randomIndex].SpawnAndDie();
        }
    }
}
