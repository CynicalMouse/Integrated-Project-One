using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        //check if collided object is player
        var player = other.collider.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // damage the player
            player.Damaged();               
        }
    }
}
