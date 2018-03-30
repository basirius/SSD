using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerController : MonoBehaviour {

    private float MovementSpeed;

    void Start () {
        GameManager gameManager = GameManager.Instance;
        MovementSpeed = gameManager.LevelSpeed;
    }


    void Update () {
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
    }

    private void TakeDamage(float damage)
    {
        
    }

    private void RestoreShield(float shield)
    {

    }

}
