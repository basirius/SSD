using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float ShotSpeed;
    public float BulletDamageModifier;
    public float Damage;
	
	void Update () {
        transform.Translate(0, 0, ShotSpeed * Time.deltaTime);
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    other.SendMessage("TakeDamage" , Damage);
    //    Destroy(gameObject);
    //}
}
