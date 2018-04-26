using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargetBehaviour : MonoBehaviour {


    private GameObject player;
    private float movementSpeed;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) < 300)
        {
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        }
	}
}
