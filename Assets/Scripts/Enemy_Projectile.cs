using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    //how much damage the bullet does  // Jason - at the moment it only deal one damage
    //[SerializeField]
    //public float damage;

    // speed of bullet
    [SerializeField]
    public float speed;

    // reference to player
    [SerializeField]
    public GameObject player;

    // reference to projectile's rigidbody
    Rigidbody2D _rigidbody;

    // direction to move
    Vector2 moveDirection;

    void Start()
    {
        // asign rigid body
        _rigidbody = GetComponent<Rigidbody2D>();

        // get direction of projectile
        moveDirection = (player.transform.position - transform.position).normalized * speed;

        // move the projectile
        _rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if collided object is player
        var player = collision.collider.GetComponent<PlayerMovement>();

        if (player != null)
        {
            // damage the player
            player.Damaged();               
        }

        // destroys projectile if it hits anything
        Destroy(gameObject);
    }

    
        
    

}
