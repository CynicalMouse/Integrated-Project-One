using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Serialize Field makes vaiables visible in editor

    // store points
    [SerializeField]
    private Transform[] _points;

    // Speed of platform
    [SerializeField]
    private float speed;

    // Distance at which the platform is considered to have reached a point
    [SerializeField]
    private float checkDistance = 0.05f;

    // Variables for which point the platform is moving to
    private Transform targetPoint;
    private int targetPointIndex = 0;



    
    void Start()
    {
        // target first point
        targetPoint = _points[0];
    }

    // Update is called once per frame
    void Update()
    {
        // move towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // when target point is reached, move to next one
        if (Vector2.Distance(transform.position, targetPoint.position) < checkDistance)
        {
            targetPoint = GetNextPoint();
        }
    }

    private Transform GetNextPoint()
    {
        // increase by one to target next point
        targetPointIndex++;

        // if number of points exceeded, go back to first point
        if (targetPointIndex >= _points.Length)
        {
            targetPointIndex = 0;
        }

        return _points[targetPointIndex];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // upon player jumping on platform it becomes a child of the platform, meaning it can stand on the platform without sliding off
        var platformMovement = other.collider.GetComponent<PlayerMovement>();
        if (platformMovement != null)
        {
            platformMovement.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // when the player comes off the platform its parent is reset (no longer child of platform)
        var platformMovement = other.collider.GetComponent<PlayerMovement>();
        if (platformMovement != null)
        {
            platformMovement.ResetParent();
        }
    }
}
