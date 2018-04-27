using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargetBehaviour : MonoBehaviour {


    private GameObject player;
    private float movementSpeed;
    private Light droneLight;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        droneLight = gameObject.GetComponentInChildren<Light>();
	}
	
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) < 300)
        {
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            droneLight.color = Color.red;
            droneLight.intensity = 10;
        }
	}
}
