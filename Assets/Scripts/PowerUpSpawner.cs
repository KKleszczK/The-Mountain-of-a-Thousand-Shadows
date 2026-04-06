using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{

    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] public List<Transform> spawnPoints;

    public float respawnDelay = 10f;

    private List<GameObject> activePowerUps;
    private List<float> respawnTimers;

    private void Start()
    {
        activePowerUps = new List<GameObject>(new GameObject[spawnPoints.Count]);
        respawnTimers = new List<float>(new float[spawnPoints.Count]);
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            SpawnPowerUp(i);
            respawnTimers.Add(0f);
        }
    }


    void SpawnPowerUp(int index)
    {
        GameObject newPowerUp = Instantiate(powerUpPrefab, spawnPoints[index].position, Quaternion.identity);
        activePowerUps[index] = newPowerUp;
    }
    void Update()
    {
        for (int i = 0; i < activePowerUps.Count; i++)
        {
            if (activePowerUps[i] == null) //powerup picked up
            {
                respawnTimers[i] += Time.deltaTime;
                if (respawnTimers[i] >= respawnDelay)
                {
                    SpawnPowerUp(i);
                    respawnTimers[i] = 0f;
                }
            }
        }
    }
}
