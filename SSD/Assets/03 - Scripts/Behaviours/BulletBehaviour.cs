using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float ShotSpeed;
	
	void Update () {
        transform.Translate(0, 0, ShotSpeed * Time.deltaTime);
    }
}
