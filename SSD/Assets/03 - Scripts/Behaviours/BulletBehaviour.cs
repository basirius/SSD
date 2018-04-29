using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float ShotSpeed;
    public float BulletDamageModifier;
    public GameObject ImpactEffect;
    [HideInInspector]
    public float Damage;
    private AudioSource shotSound;

	void Start (){
		Destroy (gameObject, 2.0f);
        shotSound = gameObject.GetComponent<AudioSource>();
        shotSound.Play();
        print(Damage);

	}

	void Update () {
        transform.Translate(0, 0, ShotSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("Hit");
            collider.gameObject.SendMessage("TakeDamage", Damage);
            Destroy(gameObject);
        }
    }
}
