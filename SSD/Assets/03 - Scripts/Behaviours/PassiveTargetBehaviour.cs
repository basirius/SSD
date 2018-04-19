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

    //private Transform Explosion;
    //private AudioSource destructionSound;
    //public Transform Collectible;
    private GameObject impactEffectObject;
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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            HitPoints -= collider.gameObject.GetComponent<BulletBehaviour>().Damage;
            impactEffectObject = collider.gameObject.GetComponent<BulletBehaviour>().ImpactEffect;
            Instantiate(impactEffectObject, collider.transform.position, collider.transform.rotation);
            if (HitPoints <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage((IsDamaging) ? "TakeDamage" : "RestoreShield", Damage);
            HitPoints = 0;
            Destroy(gameObject);
        }

    }

    float ReturnRandom()
    {
        float random = Random.Range(-0.5f, 0.5f);
        return random;
    }
}


