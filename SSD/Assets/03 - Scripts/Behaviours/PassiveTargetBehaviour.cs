using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveTargetBehaviour : MonoBehaviour
{
    [Tooltip("If set to false, the amount of Damage will restore the Shield")]
    public bool IsDamaging;
    public float Damage;
    public float HitPoints;
    public GameObject Explosion;
    public AudioSource ExplosionSound;
    [Header("Probability of being Spawned")]
    [Tooltip("select a number between 1 and 100")]
    public float SpawnProbability;
    [Header("Dust")]
    [Tooltip("Dusty")]
    public GameObject Dust;
    [HideInInspector]
    public float SpawnRelativeCalculatedWeight;

    private float randomXrotation = 0;
    private float randomYrotation = 0;
    private float randomZrotation = 0;

    private GameObject impactEffectObject;
    private bool isTargetHealthZero;
    //private float collectibleProbability;

    private bool collisionWithPlayer;
    void Start()
    {
        randomXrotation = ReturnRandom();
        randomYrotation = ReturnRandom();
        randomZrotation = ReturnRandom();
        isTargetHealthZero = false;
        //collectibleProbability = Random.value;
        collisionWithPlayer = false;
        Destroy(gameObject, 25);
    }

    void Update()
    {
        if (gameObject.tag == "Passive")
        {
            transform.Rotate(randomXrotation, randomYrotation, randomZrotation);
        }
        

        if (HitPoints <= 0 && !isTargetHealthZero)
        {
            isTargetHealthZero = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            HitPoints -= collider.gameObject.GetComponent<BulletBehaviour>().Damage;
            impactEffectObject = collider.gameObject.GetComponent<BulletBehaviour>().ImpactEffect;
            Instantiate(impactEffectObject, collider.transform.position, collider.transform.rotation);
            Destroy(collider.gameObject);

            if (HitPoints <= 0)
            {
                DestroyTheTarget();
            }
        }

        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage((IsDamaging) ? "TakeDamage" : "RestoreShield", Damage);
            HitPoints = 0;
            collisionWithPlayer = true;
            DestroyTheTarget();
        }

        if (collider.gameObject.tag == "Active")
        {
            DestroyTheTarget();
        }
    }

    float ReturnRandom()
    {
        float random = Random.Range(-0.5f, 0.5f);
        return random;
    }

    void DestroyTheTarget()
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        if (!collisionWithPlayer)
        {
            ExplosionSound.Play();
        }
        gameObject.GetComponent<Collider>().enabled = false;
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            renderer.enabled = false;
            Instantiate(Dust, transform.position, transform.rotation);
        }
        Destroy(gameObject, 2);
    }
}


