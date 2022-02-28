using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //check if collided object is player
        var player = other.collider.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // kill the player
            player.Death();
        }
    }
}
