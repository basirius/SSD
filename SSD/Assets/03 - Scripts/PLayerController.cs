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
    private Vector3 positionLimit;
    private float horizontalMoveSpeed;
    private float verticalMoveSpeed;
    private float currentShield;
    private float maximumShield;

    // Weapon
    [SerializeField]
    private Transform firePoint;
    private float nextTimeToFire = 0;
    private float fireRate;
    private float damage;
    private GameObject[] bullets;


    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        movementSpeed = gameManager.LevelSpeed;
        horizontalMoveSpeed = gameManager.HorizontalMoveSpeed;
        verticalMoveSpeed = gameManager.VerticalMoveSpeed;
        currentShield = gameManager.CurrentShield;
        maximumShield = gameManager.MaximumShield;

        fireRate = gameManager.FireRate;
        damage = gameManager.Damage;
        bullets = gameManager.Bullets;
        print(bullets.Length);

        levelUIManager = levelUIHolder.GetComponent<LevelUIManager>();
        levelUIManager.MaximumShield = this.maximumShield;
        levelUIManager.CurrentShield = this.currentShield;

    }

    void Update()
    {
        MoveShip();
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            FireWeapon();
        }

    }

    private void FireWeapon()
    {
        GameObject bulletInstance = Instantiate(bullets[0], firePoint.position, Quaternion.identity);
        nextTimeToFire = Time.time + 1f / fireRate;
        Destroy(bulletInstance, 2.0f);
        //fireSound.Play();
        //fireLight.intensity = 10.0f;
        if (Input.GetButtonUp("Fire1"))
        {
            // fireLight.intensity = 0.0f;
        }
    }

    private void MoveShip()
    {
        positionLimit = transform.position;
        positionLimit.x = Mathf.Clamp(positionLimit.x, -55.0f, 55.0f);
        positionLimit.y = Mathf.Clamp(positionLimit.y, -55.0f, 55.0f);
        transform.position = positionLimit;
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        transform.Translate(moveHorizontal * horizontalMoveSpeed, 0.0f, 0.0f);
        transform.Translate(0.0f, moveVertical * verticalMoveSpeed, 0.0f);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
    }

    private void TakeDamage(float damage)
    {
        currentShield -= damage;
        if (currentShield < 0)
        {
            currentShield = 0;
        }
        levelUIManager.CurrentShield = this.currentShield;
        print(currentShield);
    }

    private void RestoreShield(float shield)
    {
        currentShield += shield;
        if (currentShield > maximumShield)
        {
            currentShield = maximumShield;
        }
        levelUIManager.CurrentShield = this.currentShield;
        print(currentShield);
    }

}
