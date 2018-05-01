using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTargetBehaviour : MonoBehaviour
{

    public float LifeTime;
    public GameObject Projectile;
    public GameObject Gun;
    public float FixedDistanceFromThePlayer;
    public float FireRate;
    public float FiringDelay;
    private float nextTimeToFire;
    private GameObject player;
    private float movementSpeed;
    private Light droneLight;
    private float birthTime;
    private bool expired = false;
    private bool engaged = false;
    private PassiveTargetBehaviour passiveTargetBehaviour;


    void Start()
    {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        player = GameObject.FindGameObjectWithTag("Player");
        droneLight = gameObject.GetComponentInChildren<Light>();
        nextTimeToFire = FiringDelay;
        birthTime = Time.time;
        passiveTargetBehaviour = GetComponent<PassiveTargetBehaviour>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < FixedDistanceFromThePlayer && !expired && !engaged)
        {
            Engage();

        }

        if (Time.time > birthTime + LifeTime || passiveTargetBehaviour.HitPoints <=0)
        {
            Disengage();
        }
    }

    void Engage()
    {
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
        droneLight.color = Color.red;
        droneLight.intensity = 10;
        if (Time.time > nextTimeToFire)
        {
            OpenFire();
        }
    }

    void Disengage()
    {
        expired = true;
        engaged = false;
        transform.Translate(0, 0, 0);
        droneLight.color = Color.blue;
        droneLight.intensity = 15;
    }

    private void OpenFire()
    {
        Gun.transform.LookAt(player.transform);
        var laser = Instantiate(Projectile, Gun.transform.position, Gun.transform.rotation);
        nextTimeToFire = Time.time + 1f / FireRate;
    }
}
