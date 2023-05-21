using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;
    private Vector3 spawnObstaclePosition;
    private Vector3 spawnCoinPosition;

    float timer = 0;
    bool timerReached = false;

    void Update()
    {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, spawnObstaclePosition);
        if (distanceToHorizon < 120) {
            spawnObstacles();
        }

        if (!timerReached) {
            timer += Time.deltaTime;
            if (timer >= 5) {
                Debug.Log("Coin is spawned");
                spawnCoins();
                timer = 0;
            }
        }
    }

    void spawnObstacles() {
        spawnObstaclePosition = new Vector3(0, 0, spawnObstaclePosition.z + 30);
        Instantiate(obstaclePrefabs[(Random.Range(0,obstaclePrefabs.Length))], spawnObstaclePosition, Quaternion.identity);
    }

    void spawnCoins() {
        spawnCoinPosition = new Vector3(0, 0, player.gameObject.transform.position.z + 25);
        Instantiate(coinPrefab, spawnCoinPosition, Quaternion.identity);
    }
}
