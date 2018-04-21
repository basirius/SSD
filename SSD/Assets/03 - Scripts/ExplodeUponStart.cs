using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeUponStart : MonoBehaviour {

    public FracturedObject ObjectToExplode;
    public float ExplosionForce;
	void Start () {
        
	}

	void Update () {
            ObjectToExplode.Explode(ObjectToExplode.transform.position, ExplosionForce);
	}
}
