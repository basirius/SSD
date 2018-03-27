using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelBehaviour : MonoBehaviour {

    public float MovementSpeed;
    GameManager gameManager = GameManager.Instance;

	void Start () {
        MovementSpeed = gameManager.TunnelSpawnSpeed;
        print(MovementSpeed);
	}
	
	
	void Update () {
		
	}
}
