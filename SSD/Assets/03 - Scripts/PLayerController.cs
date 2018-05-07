using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour
{

    // UI
    [SerializeField]
    private GameObject levelUIHolder;
    [SerializeField]
    private LevelUIManager levelUIManager;

    // Ship
    private float movementSpeed;
    private float moveHorizontal = 0;
    private float moveVertical = 0;
    private float tiltHorizontal = 0;
    private Vector3 positionLimit;
    private float horizontalMoveSpeed;
    private float verticalMoveSpeed;
    private float tiltAngle;
    private float tiltSmooth;
    private float currentShield;
    private float maximumShield;
    private float structuralHealth;

    // Weapon
    [SerializeField]
    private Transform firePoint;
    private float nextTimeToFire = 0;
    private float fireRate;
    private GameObject[] bullets;

    // Music and Audio
    private GameObject levelMusicObject;
    [SerializeField]
    private AudioSource takeDamageSound;
    [SerializeField]
    private AudioSource restoreShieldSound;

    // Dictionary of all the children game objects
    private Dictionary<string, Transform> childrenDictionary = new Dictionary<string, Transform>();

    //Inventory
    private InventoryManager inventoryManager;


    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        movementSpeed = gameManager.LevelSpeed;
        horizontalMoveSpeed = gameManager.HorizontalMoveSpeed;
        verticalMoveSpeed = gameManager.VerticalMoveSpeed;
        currentShield = gameManager.CurrentShield;
        maximumShield = gameManager.MaximumShield;
        structuralHealth = gameManager.StructuralHealth;
        tiltAngle = gameManager.TiltAngle;
        tiltSmooth = gameManager.TiltSmooth;

        fireRate = gameManager.FireRate;
        bullets = gameManager.Bullets;

        // the music object don't make it here. I don't know why!
        //levelMusicObject = gameManager.LevelMusicObject;

        //if (!levelMusicObject)
        //{
        //    print("Not Found");
        //} else
        //{
        //    print(levelMusicObject.name);
        //}

        levelUIManager = levelUIHolder.GetComponent<LevelUIManager>();
        levelUIManager.MaximumShield = this.maximumShield;
        levelUIManager.CurrentShield = this.currentShield;

        // populate the children dictionary
        foreach (Transform t in transform)
        {
            childrenDictionary.Add(t.name, t);
        }
        inventoryManager = gameManager.Inventory.GetComponent<InventoryManager>();
    }

    void Update()
    {
        MoveShip();
        
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            FireWeapon();
        }

        if (structuralHealth < 0)
        {
            LevelUIManager.Instance.Restart();
        }

    }

    private void FireWeapon()
    {
        Transform MuzzleFire = childrenDictionary["MachineGunLight"];
        Transform muzzleFireInstance = Instantiate(MuzzleFire, firePoint.transform.position, transform.rotation);
        muzzleFireInstance.gameObject.SetActive(true);
        Destroy(muzzleFireInstance.gameObject, 0.1f);
        GameObject bulletInstance = Instantiate(bullets[0], firePoint.position, Quaternion.identity);
        nextTimeToFire = Time.time + 1f / fireRate;
        Destroy(bulletInstance, 2.0f);
    }

    private void MoveShip()
    {
        positionLimit = transform.position;
        positionLimit.x = Mathf.Clamp(positionLimit.x, -55.0f, 55.0f);
        positionLimit.y = Mathf.Clamp(positionLimit.y, -55.0f, 55.0f);
        transform.position = positionLimit;
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        tiltHorizontal = -moveHorizontal * tiltAngle;
        // Tilt
        Quaternion tilt = Quaternion.Euler(0, 0, tiltHorizontal);
        transform.rotation = Quaternion.Slerp(transform.rotation, tilt, Time.deltaTime * tiltSmooth);
        // Move
        transform.Translate(moveHorizontal * horizontalMoveSpeed, moveVertical * verticalMoveSpeed, movementSpeed * Time.deltaTime);
    }

    private void TakeDamage(float damage)
    {
        currentShield -= damage;
        if (currentShield < 0)
        {
            currentShield = 0;
        }

        if (currentShield == 0)
        {
            levelUIManager.CurrentShield = this.currentShield;
            Transform structuralDamageEffect = childrenDictionary["StructuralDamageEffect"];
            Transform structuralDamageEffectInstance = Instantiate(structuralDamageEffect, transform.position, transform.rotation, gameObject.transform);
            structuralDamageEffectInstance.gameObject.SetActive(true);
            structuralHealth -= damage;
            takeDamageSound.Play();
        }
        else
        {
            levelUIManager.CurrentShield = this.currentShield;
            Transform shieldDamageEffect = childrenDictionary["PlasmaExplosionEffect"];
            Transform shieldDamageEffectInstance = Instantiate(shieldDamageEffect, transform.position, transform.rotation);
            shieldDamageEffectInstance.gameObject.SetActive(true);
            takeDamageSound.Play();
        }

    }

    private void RestoreShield(float shield)
    {
        currentShield += shield;
        if (currentShield > maximumShield)
        {
            currentShield = maximumShield;
        }
        levelUIManager.CurrentShield = this.currentShield;
        Transform shieldRestoreEffect = childrenDictionary["ShieldRestoreEffect"];
        Transform shieldRestoreEffectInstance = Instantiate(shieldRestoreEffect, transform.position, transform.rotation);
        shieldRestoreEffectInstance.gameObject.SetActive(true);
        restoreShieldSound.Play();
    }


    private void Collect(int DustIndex)
    {
        switch (DustIndex)
        {
            case 1:
                inventoryManager.DustMineral_1 += 1;
                break;
            case 2:
                inventoryManager.DustMineral_2 += 1;
                break;
            case 3:
                inventoryManager.DustMineral_3 += 1;
                break;
            case 4:
                inventoryManager.DustMineral_4 += 1;
                break;
            case 5:
                inventoryManager.DustMineral_5 += 1;
                break;
            default:
                break;
        }
    }

}
