using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

    public float ShotSpeed;
    public GameObject ImpactEffect;
    public float LaserDamage;
    private AudioSource shotSound;

    void Start()
    {
        //Destroy(gameObject, 2.0f);
        shotSound = gameObject.GetComponent<AudioSource>();
        shotSound.Play();
    }

    void Update()
    {
        transform.Translate(0, 0, ShotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage("TakeDamage", LaserDamage);
            Destroy(gameObject);
        }
    }

}
