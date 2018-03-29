using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBehaviour : MonoBehaviour {

    // Variables
    private float MovementSpeed;
    private float nextSpawnTime = 0;
    private float spawnPositionX;
    private float spawnPositionY;
    private float randomSpawnerSelector;
    private float spawnInterval;
    private float tunnelSegmentLength;
    private int tunnelSegmentSpawnPerSecond;
    private int numberOftunnelSegmentSpawnsInEachInterval;

    // Game Objects
    private GameObject tunnelSegment;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        MovementSpeed = gameManager.LevelSpeed;
        tunnelSegment = gameManager.TunnelSegment;
        tunnelSegmentLength = gameManager.TunnelSegmentLength;
        spawnInterval = gameManager.SpawnInterval;
        tunnelSegmentSpawnPerSecond = (int)(MovementSpeed / tunnelSegmentLength);
        numberOftunnelSegmentSpawnsInEachInterval = (int)(tunnelSegmentSpawnPerSecond * spawnInterval);
    }
	
	void Update () {
        randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        Spawn();
    }

    void Spawn()
    {
        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            SpawnTunnelSection();
            nextSpawnTime += spawnInterval;
        }
    }


    /// <summary>
    /// Spawn a tunnel section made of tunnel segments
    /// </summary>
    void SpawnTunnelSection()
    {
        Vector3 tunnelSegmentSpawnPosition = new Vector3();
        tunnelSegmentSpawnPosition = transform.position;

        for (int i = 0; i < numberOftunnelSegmentSpawnsInEachInterval; i++)
        {
            tunnelSegmentSpawnPosition.z = transform.position.z + i * 80;
            Instantiate(tunnelSegment, tunnelSegmentSpawnPosition, Quaternion.identity);
        }
    }

}
