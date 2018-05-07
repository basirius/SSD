using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustBehaviour : MonoBehaviour {

    [HideInInspector]
    public GameObject Player;
    public float MovementSpeed;
    public int DustIndex;
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, 3.0f);
	}
	
	void Update () {
        transform.LookAt(Player.transform);
        transform.Translate(0, 0, MovementSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.SendMessage("Collect", DustIndex);
            Destroy(gameObject);
        }
    }
}
