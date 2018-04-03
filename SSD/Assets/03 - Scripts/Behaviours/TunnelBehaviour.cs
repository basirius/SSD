using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TunnelBehaviour : MonoBehaviour {

    #region Variables

    private float nextSpawnTime = 0;
    private float spawnPositionX;
    private float spawnPositionY;
    private float randomSpawnerSelector;
    private float spawnInterval;
    private bool spawnStartFlag;
    private float spawnStartDelay;
    private float totalSpawnProbability;

    private float MovementSpeed;
    private float tunnelSegmentLength;
    private int tunnelSegmentSpawnPerSecond;
    private int numberOftunnelSegmentSpawnsInEachInterval;
    #endregion

    // Game Objects
    private GameObject tunnelSegment;
    private GameObject[] spawnGameObjects;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        this.MovementSpeed = gameManager.LevelSpeed;
        this.tunnelSegment = gameManager.TunnelSegment;
        this.tunnelSegmentLength = gameManager.TunnelSegmentLength;
        this.spawnInterval = gameManager.SpawnInterval;
        this.spawnStartDelay = gameManager.StartSpawnDelay;
        this.spawnGameObjects = gameManager.SpawnGameObjects;
        this.totalSpawnProbability = 0;
        tunnelSegmentSpawnPerSecond = (int)(MovementSpeed / tunnelSegmentLength);
        numberOftunnelSegmentSpawnsInEachInterval = (int)(tunnelSegmentSpawnPerSecond * 5); // this is the hard coded spawn interval. Make it parametric
        SpawnTunnelSection(-1560); // this creates an initial portion of the tunnel
        Invoke("StartSpawning", spawnStartDelay);

        // these two loops normalise the probability of spawning each target
        foreach (GameObject gameObject in spawnGameObjects)
        {
            PassiveTargetBehaviour spawnBehaviour = gameObject.GetComponent<PassiveTargetBehaviour>();
            totalSpawnProbability += spawnBehaviour.SpawnProbability;
        }

        foreach (GameObject gameObject in spawnGameObjects)
        {
            PassiveTargetBehaviour spawnBehaviour = gameObject.GetComponent<PassiveTargetBehaviour>();
            spawnBehaviour.SpawnRelativeCalculatedWeight = (spawnBehaviour.SpawnProbability) / totalSpawnProbability;
            print(spawnBehaviour.SpawnRelativeCalculatedWeight);
        }
    }
	
	void Update () {
        randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        Spawn(SpawnIndexSelector());
    }

    int SpawnIndexSelector()
    {
        float rand = Random.value;
        

        foreach (GameObject gameObject in spawnGameObjects)
        {
            PassiveTargetBehaviour spawnBehaviour = gameObject.GetComponent<PassiveTargetBehaviour>();
            // develop logic to find the index. 
        }


        return 1;
    }

    /// <summary>
    /// Sets the flag to start spawning the tunnel sections and game objects
    /// </summary>
    void StartSpawning()
    {
        spawnStartFlag = true;
    }




    void Spawn(int spawnIndex)
    {
        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            SpawnTunnelSection(0);
            nextSpawnTime += spawnInterval;
            SpawnPassiveObjects(spawnIndex);
        }
    }

    void SpawnPassiveObjects(int spawnIndex)
    {
        if (spawnStartFlag)
        {
            spawnPositionX = Random.Range(-55, 55);
            spawnPositionY = Random.Range(-55, 55);
            Vector3 spawnPositionVector = new Vector3(spawnPositionX, spawnPositionY, transform.position.z);
            Instantiate(spawnGameObjects[spawnIndex], spawnPositionVector, Quaternion.identity);
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
