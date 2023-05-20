using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject player;
    public GameObject[] obstaclePrefabs;
    private Vector3 spawnObstaclePosition;

    void Update()
    {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, spawnObstaclePosition);
        if (distanceToHorizon < 120) {
            spawnObstacles();
        }
    }

    void spawnObstacles() {
        spawnObstaclePosition = new Vector3(0, 0, spawnObstaclePosition.z + 30);
        Instantiate(obstaclePrefabs[(Random.Range(0,obstaclePrefabs.Length))], spawnObstaclePosition, Quaternion.identity);
    }
}
