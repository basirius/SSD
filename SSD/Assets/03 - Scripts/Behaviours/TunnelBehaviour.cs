using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBehaviour : MonoBehaviour {

    public float MovementSpeed;
    private float nextSpawnTime = 0;
    private float spawnPositionX;
    private float spawnPositionY;
    private float randomSpawnerSelector;
    private float spawnInterval;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        MovementSpeed = gameManager.TunnelSpawnSpeed;
        
	}
	
	
	void Update () {
        randomSpawnerSelector = Random.Range(1, 100);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
    }


    void SpawnTarget(int targetIndex)
    {
        if (Time.timeSinceLevelLoad > nextSpawnTime)
        {
            // Instantiate the targer
            nextSpawnTime += spawnInterval;
        }
    }

}
