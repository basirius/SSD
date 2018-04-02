using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTargetBehaviour : MonoBehaviour
{
    public bool IsDamaging;
    public float Damage;
    public int HitPoints;

    private float randomXrotation = 0;
    private float randomYrotation = 0;
    private float randomZrotation = 0;

    //public Transform Explosion;
    //private AudioSource destructionSound;
    // public Transform Collectible;
    private bool isTargetHealthZero;
    private float collectibleProbability;

    void Start()
    {
        randomXrotation = ReturnRandom();
        randomYrotation = ReturnRandom();
        randomZrotation = ReturnRandom();
        isTargetHealthZero = false;
        //destructionSound = transform.GetComponent<AudioSource>();
        collectibleProbability = Random.value;
        Destroy(gameObject, 100);
    }

    void Update()
    {
        transform.Rotate(randomXrotation, randomYrotation, randomZrotation);

        if (HitPoints <= 0 && !isTargetHealthZero)
        {
            //Instantiate(Explosion, transform.position, Quaternion.identity);
            //destructionSound.Play();

            isTargetHealthZero = true;
            //if (collectibleProbability > 0.0f)
            //{
            //    Instantiate(Collectible, transform.position, Quaternion.identity);
            //}
            Destroy(gameObject, 0.3f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            HitPoints -= 20;
            // change this to take damage equal to the projectile damage amount
        }

        if (other.tag == "Player")
        {
            other.SendMessage((IsDamaging) ? "TakeDamage" : "HealShield", Damage);
            HitPoints = 0;
        }

    }

    float ReturnRandom()
    {
        float random = Random.Range(-0.5f, 0.5f);
        return random;
    }
}


