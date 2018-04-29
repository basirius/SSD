using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargetBehaviour : MonoBehaviour {

    public float LifeTime;
    public GameObject Projectile;
    public GameObject Gun;

    private GameObject player;
    private float movementSpeed;
    private Light droneLight;
    private float birthTime;
    private bool expired = false;

    private bool fireOnce = true;
    

    void Start () {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        droneLight = gameObject.GetComponentInChildren<Light>();
        birthTime = Time.time;
	}
	
	void Update () {
        if (Vector3.Distance(transform.position, player.transform.position) < 300 && !expired)
        {
            transform.Translate(0, 0, movementSpeed * Time.deltaTime);
            droneLight.color = Color.red;
            droneLight.intensity = 10;
            if (fireOnce)
            {
                OpenFire();
            }
            
        }

        if (Time.time > birthTime + LifeTime)
        {
            expired = true;
            transform.Translate(0, 0, 0);
            droneLight.color = Color.blue;
            droneLight.intensity = 15;
        }
	}

    private void OpenFire()
    {
        Gun.transform.LookAt(player.transform);
        var laser = Instantiate(Projectile, Gun.transform.position, Gun.transform.rotation);
        laser.transform.Translate(0, 0, 800 * Time.deltaTime);
        fireOnce = false;
    }
}
