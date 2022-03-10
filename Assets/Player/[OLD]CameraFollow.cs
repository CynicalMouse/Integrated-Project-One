using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Folllow the player character
    public Transform followTransform;


    // Update is called once per frame
    void FixedUpdate()
    {
        // change camera location to player location
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);

    }
}
