using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour {

    [SerializeField]
    private GameObject levelUIHolder;
    [SerializeField]
    private Transform firePoint;
    private LevelUIManager levelUIManager;
    private float movementSpeed;
    private float moveHorizontal = 0;
    private float moveVertical = 0;
    private float horizontalMoveSpeed;
    private float verticalMoveSpeed;
    private float currentShield;
    private float maximumShield;
    private float nextTimeToFire = 0;
    private float fireRate = 15;
    private GameObject projectile;

    private Vector3 positionLimit;
    private GameManager gameManager;

    void Start () {
        gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        horizontalMoveSpeed = gameManager.HorizontalMoveSpeed;
        verticalMoveSpeed = gameManager.VerticalMoveSpeed;
        currentShield = gameManager.CurrentShield;
        maximumShield = gameManager.MaximumShield;
        levelUIManager = levelUIHolder.GetComponent<LevelUIManager>();
        levelUIManager.MaximumShield = this.maximumShield;
        levelUIManager.CurrentShield = this.currentShield;
    }

    void Update () {
        MoveShip();
    }

    private void FireWeapon()
    {
        if (Input.GetButton("Fire1") && Time.time > nextTimeToFire)
        {
            GameObject projectileInstance = Instantiate(projectile, firePoint.position, Quaternion.identity, transform);
            Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
            rb.AddForce(0.0f, 0.0f, 30000.0f);
            nextTimeToFire = Time.time + 1f / fireRate;
            Destroy(projectileInstance, 2.0f);
            //fireSound.Play();
            //fireLight.intensity = 10.0f;
        }
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
