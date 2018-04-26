using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPointBehaviour : MonoBehaviour {

    public float PositionClamp;
    private Vector3 positionLimit;

    void Update () {

        positionLimit = transform.position;
        positionLimit.x = Mathf.Clamp(positionLimit.x, -PositionClamp, PositionClamp);
        positionLimit.y = Mathf.Clamp(positionLimit.y, -PositionClamp, PositionClamp);
        transform.position = positionLimit;

    }
}
