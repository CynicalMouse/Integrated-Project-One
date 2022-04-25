using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Set an animation for player death (side note: will later try move it back to the player movement script)
    public Animator healthAnim;
    public Animator barAnim;
    public Animator playerAnim;

    private void OnCollisionEnter2D(Collision2D other)
    {
        //check if collided object is player
        var player = other.collider.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // kill the player
            player.Damaged();

            // sets their health to zero
            healthAnim.SetBool("IsAlive", false);
            barAnim.SetBool("IsAlive", false);
            playerAnim.SetBool("IsAlive", false);
        }
    }
}
