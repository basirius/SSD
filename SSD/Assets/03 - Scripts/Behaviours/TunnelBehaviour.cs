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

    // Game Objects
    private GameObject tunnelSegment;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        MovementSpeed = gameManager.LevelSpeed;
        tunnelSegment = gameManager.TunnelSegment;
        spawnInterval = gameManager.SpawnInterval;
        print(tunnelSegment.transform.name);
        
	}
	
	
	void Update () {
        randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        SpawnTarget();
    }


    void SpawnTarget()
    {
        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            Instantiate(tunnelSegment, transform.position, Quaternion.identity);
            nextSpawnTime += spawnInterval;
        }
    }

}
