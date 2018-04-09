using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTargetBehaviour : MonoBehaviour
{
    public bool IsDamaging;
    public float Damage;
    public float HitPoints;
    [Header("Probability of being Spawned")]
    [Tooltip("select a number between 1 and 100")]
    public float SpawnProbability;
    [HideInInspector]
    public float SpawnRelativeCalculatedWeight;

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
        Destroy(gameObject, 20);
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
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            print("Hit th bullet");
            HitPoints -= collision.gameObject.GetComponent<BulletBehaviour>().Damage;
            Destroy(gameObject);
            print(HitPoints);

        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage((IsDamaging) ? "TakeDamage" : "RestoreShield", Damage);
            HitPoints = 0;
        }

    }


    //void OnCollisionEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        other.SendMessage((IsDamaging) ? "TakeDamage" : "RestoreShield", Damage);
    //        HitPoints = 0;
    //    }

    //    if (other.tag == "Projectile")
    //    {
    //        print("Hit th bullet");
    //        HitPoints -= other.GetComponent<BulletBehaviour>().Damage;
    //    }
    //}

    float ReturnRandom()
    {
        float random = Random.Range(-0.5f, 0.5f);
        return random;
    }
}


