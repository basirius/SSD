using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class TunnelBehaviour : MonoBehaviour {

    #region Variables
    private float nextTargetSpawnTime = 0;
    private float nextTunnelSpawnTime = 0;
    private float spawnPositionX;
    private float spawnPositionY;
    private float spawnInterval;
    private float tunnelSpawnInterval;
    private bool spawnStartFlag;
    private float spawnStartDelay;
    private float totalSpawnProbability;
    private float MovementSpeed;
    private float tunnelSegmentLength;
    private int tunnelSegmentSpawnPerSecond;
    private int numberOftunnelSegmentSpawnsInEachInterval;
    Vector3 tunnelSegmentSpawnPosition = new Vector3();
    private SortedDictionary<float, int> sortedSpawnProbabilityDictionary = new SortedDictionary<float, int>();
    private SortedDictionary<float, int> sortedAdditiveSpawnProbabilityDictionary = new SortedDictionary<float, int>();
    #endregion

    #region Game Objects
    private GameObject tunnelSegment;
    private GameObject[] spawnGameObjects;
    #endregion

    void Start () {
        GameManager gameManager = GameManager.Instance;
        this.MovementSpeed = gameManager.LevelSpeed;
        this.tunnelSegment = gameManager.TunnelSegment;
        this.tunnelSegmentLength = gameManager.TunnelSegmentLength;
        this.spawnInterval = gameManager.SpawnInterval;
        this.tunnelSpawnInterval = gameManager.TunnelSpawnInterval;
        this.spawnStartDelay = gameManager.StartSpawnDelay;
        this.spawnGameObjects = gameManager.SpawnGameObjects;
        this.totalSpawnProbability = 0;
        tunnelSegmentSpawnPerSecond = (int)(MovementSpeed / tunnelSegmentLength);
        numberOftunnelSegmentSpawnsInEachInterval = (int)(tunnelSegmentSpawnPerSecond * tunnelSpawnInterval);
        tunnelSegmentSpawnPosition = Vector3.zero;
        SpawnTunnelSection(-1560); // this creates an initial portion of the tunnel
        Invoke("StartSpawning", spawnStartDelay);

        // these two loops normalise the probability of spawning each target
        foreach (GameObject gameObject in spawnGameObjects)
        {
                PassiveTargetBehaviour spawnBehaviour = gameObject.GetComponent<PassiveTargetBehaviour>();
                totalSpawnProbability += spawnBehaviour.SpawnProbability;
        }

        for (int i = 0; i < spawnGameObjects.Length; i++)
        {
            PassiveTargetBehaviour spawnBehaviour = spawnGameObjects[i].GetComponent<PassiveTargetBehaviour>();
            float weight = (spawnBehaviour.SpawnProbability) / totalSpawnProbability;
            spawnBehaviour.SpawnRelativeCalculatedWeight = weight;
            sortedSpawnProbabilityDictionary.Add(weight, i);
        }
        float additiveWeight = 0;
        float keyHolder;
        int valueHolder;
        foreach (var item in sortedSpawnProbabilityDictionary)
        {
            keyHolder = item.Key + additiveWeight;
            additiveWeight = keyHolder;
            valueHolder = item.Value;
            sortedAdditiveSpawnProbabilityDictionary.Add(keyHolder, valueHolder);
        }
    }
	
	void Update () {
        // randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
        SpawnTargets(SpawnIndexSelector());
        SpawnTunnel();
    }


    /// <summary>
    /// This method returns the index of a game object to be spawned based on the weighted probability and a random selector
    /// </summary>
    /// <returns> the index of the game object to spawn for the passive targets</returns>
    int SpawnIndexSelector()
    {
        float random = Random.value;

        foreach (var item in sortedAdditiveSpawnProbabilityDictionary)
        {
            if (random < item.Key)
            {
                return item.Value;
            }
        }
        int defaultSpawnGameObjectIndex = sortedSpawnProbabilityDictionary.Values.Last();
        return defaultSpawnGameObjectIndex;
        
    }

    /// <summary>
    /// Sets the flag to start spawning the tunnel sections and game objects
    /// </summary>
    void StartSpawning()
    {
        spawnStartFlag = true;
    }


    void SpawnTunnel()
    {
        if (Time.timeSinceLevelLoad > nextTunnelSpawnTime)
        {
            SpawnTunnelSection(0);
            nextTunnelSpawnTime += tunnelSpawnInterval;
        }
    }

    void SpawnTargets(int spawnIndex)
    {
        if (Time.timeSinceLevelLoad > nextTargetSpawnTime)
        {
            nextTargetSpawnTime += spawnInterval;
            SpawnPassiveObjects(spawnIndex);
        }
    }

    void SpawnPassiveObjects(int spawnIndex)
    {
        if (spawnStartFlag)
        {
            GameObject spawn = spawnGameObjects[spawnIndex];
            
            spawnPositionX = Random.Range(-55, 55);
            spawnPositionY = Random.Range(-55, 55);
            Vector3 spawnPositionVector = new Vector3(spawnPositionX, spawnPositionY, transform.position.z);
            Instantiate(spawn, spawnPositionVector, Quaternion.identity);
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
        

        // tunnelSegmentSpawnPosition = transform.position;

        for (int i = 0; i < numberOftunnelSegmentSpawnsInEachInterval; i++)
        {
            tunnelSegmentSpawnPosition.z = transform.position.z + i * tunnelSegmentLength + startingPoint;
            Instantiate(tunnelSegment, tunnelSegmentSpawnPosition, Quaternion.identity);
        }
    }

}
