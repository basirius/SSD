using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour {

    private float movementSpeed;
    private float moveHorizontal = 0;
    private float moveVertical = 0;
    private float horizontalMoveSpeed;
    private float verticalMoveSpeed;
    private float currentShield;
    private float maximumShield;
    private Vector3 positionLimit;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        horizontalMoveSpeed = gameManager.HorizontalMoveSpeed;
        verticalMoveSpeed = gameManager.VerticalMoveSpeed;
        currentShield = gameManager.CurrentShield;
        maximumShield = gameManager.MaximumShield;
    }

    void Update () {
        MoveShip();
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
        print(currentShield);
    }

    private void RestoreShield(float shield)
    {
        currentShield += shield;
        print(currentShield);
    }

}
