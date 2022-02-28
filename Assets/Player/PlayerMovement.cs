using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Creating Variables

    private Rigidbody2D _rigidbody;

    // Used to stand on and move with moving platforms instead of sliding off
    private Transform _originalParent;

    // used for death and respawn
    private Collider2D _collider;
    private Vector2 _respawnPoint;

    // SerializeField allows varaibles to show up in editor
    // Edit these values in the editor as it seems editing in script doesnt do anything
    

    //GROUND CHECK//
    // used later to check if touching ground
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.05f;


    //PLAYER ATTRIBUTES//
    //player speed
    [SerializeField]
    private float speed = 2f;

    // jump height
    [SerializeField]
    private float jumpForce = 10f;

    // max number of jumps
    [SerializeField]
    private int maxJumps = 2;

    // Number of jumps left to use
    private int jumpsLeft;

    // used for collision with tilemap tiles
    [SerializeField]
    private LayerMask collisionMask;

    // DEATH AND RESPAWN //
    // used for death
    [SerializeField]
    private bool alive = true;


    // INITIAL SET UP //
    void Start()
    {
        // get rigidbody for later usage
        _rigidbody = GetComponent<Rigidbody2D>();

        // asigning original parent
        _originalParent = transform.parent;

        _collider = GetComponent<Collider2D>();

        // set initial jumpcount
        jumpsLeft = maxJumps;

        //Set respawn point to characters starting point
        SetRespawnPoint(transform.position);
    }


    // MOVEMENT //
    void Update()
    {
        // checks if alive before any movement can take place
        if (!alive)
        {
            return;
        }

        // define inputs        wasd, arrow keys to move, space to jump
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInputPressed = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");

        // move in direction pressed
        _rigidbody.velocity = new Vector2(inputX * speed, _rigidbody.velocity.y);

        // resets remaining jumps when touching ground
        if (IsGrounded() && _rigidbody.velocity.y <= 0)
        {
            jumpsLeft = maxJumps;
        }

        // if jumpkey is pressed, touching ground or has a remaining jump, jump
        if (jumpInputPressed && jumpsLeft > 0)
        {
            //jump
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpForce);
            // take away a jump
            jumpsLeft -= 1;
        }

        // Varaible Jump Height
        // If the jump key is released, immediately stop jumping
        if (jumpInputReleased && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y / 2.5f);
        }
        // if the horizontal input = 0, flips character so it always looks the way its walking 
        if (inputX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
        }
    }
    private bool IsGrounded()
    {
        // check if grounded
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionMask);
    }


    // STANDING ON BLOCKS //
    public void SetParent(Transform newParent)
    {
        // called in the MovingPlatform.cs script
        // upon player jumping on platform it becomes a child of the platform, meaning it can stand on the platform without sliding off
        _originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        // called in the MovingPlatform.cs script
        // when the player comes off the platform its parent is reset (no longer child of platform)
        transform.parent = _originalParent;
    }


    //DEATH AND RESPAWN//
        
    public void SetRespawnPoint (Vector2 position)
    {
        _respawnPoint = position;
    }
    public void Death()
    {
        // death
        alive = false;
        // disable collider
        _collider.enabled = false;

        // put trigger for death animation here

        // begin respawning the player
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        // wait for moment
        yield return new WaitForSeconds(1f);
        // move player to respawn point
        transform.position = _respawnPoint;
        // allow movement
        alive = true;
        // allow collision
        _collider.enabled = true;
    }
}
