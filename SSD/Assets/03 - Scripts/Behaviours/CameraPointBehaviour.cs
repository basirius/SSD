using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointBehaviour : MonoBehaviour {

    private float movementSpeed;
    private float moveHorizontal = 0;
    private float moveVertical = 0;
    private float horizontalMoveSpeed;
    private float verticalMoveSpeed;
    private Vector3 positionLimit;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        movementSpeed = gameManager.LevelSpeed;
        horizontalMoveSpeed = gameManager.HorizontalMoveSpeed;
        verticalMoveSpeed = gameManager.VerticalMoveSpeed;
    }
	

	void Update () {
        MovePoint();
	}

    private void MovePoint()
    {
        positionLimit = transform.position;
        positionLimit.x = Mathf.Clamp(positionLimit.x, -20.0f, 20.0f);
        positionLimit.y = Mathf.Clamp(positionLimit.y, -20.0f, 20.0f);
        transform.position = positionLimit;
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        transform.Translate(moveHorizontal * horizontalMoveSpeed, 0.0f, 0.0f);
        transform.Translate(0.0f, moveVertical * verticalMoveSpeed, 0.0f);
        transform.Translate(0, 0, movementSpeed * Time.deltaTime);
    }
}
