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
    private bool spawnStartFlag;
    private float spawnStartDelay;

    // Game Objects
    private GameObject tunnelSegment;
    private GameObject[] SpawnGameObjects;




    void Start () {
        GameManager gameManager = GameManager.Instance;
        this.MovementSpeed = gameManager.LevelSpeed;
        this.tunnelSegment = gameManager.TunnelSegment;
        this.tunnelSegmentLength = gameManager.TunnelSegmentLength;
        this.spawnInterval = gameManager.SpawnInterval;
        this.spawnStartDelay = gameManager.StartSpawnDelay;
        this.SpawnGameObjects = gameManager.SpawnGameObjects;
        tunnelSegmentSpawnPerSecond = (int)(MovementSpeed / tunnelSegmentLength);
        numberOftunnelSegmentSpawnsInEachInterval = (int)(tunnelSegmentSpawnPerSecond * 5); // this is the hard coded spawn interval. Make it parametric
        SpawnTunnelSection(-1560); // this creates an initial portion of the tunnel
        Invoke("StartSpawning", spawnStartDelay);
    }
	
	void Update () {
        randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        Spawn();
    }

    /// <summary>
    /// Sets the flag to start spawning the tunnel sections and game objects
    /// </summary>
    void StartSpawning()
    {
        spawnStartFlag = true;
    }

    void Spawn()
    {
        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            SpawnTunnelSection(0);
            nextSpawnTime += spawnInterval;
            SpawnPassiveObjects();
        }
    }

    void SpawnPassiveObjects()
    {
        if (spawnStartFlag)
        {
            spawnPositionX = Random.Range(-55, 55);
            spawnPositionY = Random.Range(-55, 55);
            Vector3 spawnPositionVector = new Vector3(spawnPositionX, spawnPositionY, transform.position.z);
            Instantiate(SpawnGameObjects[0], spawnPositionVector, Quaternion.identity);
        }
    }

    void SpawnActiveObjects()
    {
        // Check for spawnStartFlag
    }

    /// <summary>
    /// Spawn a tunnel section made of tunnel segments
    /// </summary>
    void SpawnTunnelSection(float startingPoint)
    {
        Vector3 tunnelSegmentSpawnPosition = new Vector3();
        tunnelSegmentSpawnPosition = transform.position;

        for (int i = 0; i < numberOftunnelSegmentSpawnsInEachInterval; i++)
        {
            tunnelSegmentSpawnPosition.z = transform.position.z + i * 80 + startingPoint;
            Instantiate(tunnelSegment, tunnelSegmentSpawnPosition, Quaternion.identity);
        }
    }

}
