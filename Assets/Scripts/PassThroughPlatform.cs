using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    // store reference to collider
    private Collider2D _collider;

    // store if player is standing on platform
    private bool Standing;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(Standing && Input.GetAxisRaw("Vertical") < 0)
        {
            // disable collider
            _collider.enabled = false;

            // start timer
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        // wait for 1 second
        yield return new WaitForSeconds(1.0f);

        // re enable colldier
        _collider.enabled = true;
    }

    private void PlayerOnPlatform(Collision2D other, bool value)
    {
        // get player components 
        var player = other.gameObject.GetComponent<PlayerMovement>();

        // is the object that collided the player
        if (player != null)
        {
            Standing = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        PlayerOnPlatform(other, false);
    }
}
