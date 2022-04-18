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


    [SerializeField]
    public Transform ThrowDirection;

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

    // used for collision with pick up blocks
    [SerializeField]
    private LayerMask PickUpcollisionMask;

    // DEATH AND RESPAWN //
    // used for death
    [SerializeField]
    private bool alive = true;

    // ANIMATION //
    public Animator playerAnim;
    public Animator healthAnim;
    public Animator barAnim;

    // flips sprite when going left
    public SpriteRenderer spi;

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
            //transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 1);
            ThrowDirection.localPosition = new Vector3(Mathf.Sign(inputX), 1, 1);
        }
        if (inputX > 0)
        {
            spi.flipX = false;
        }

        if (inputX < 0)
        {
            spi.flipX = true;
        }

        // trigger sprite animation
        Animation();
    }
    private bool IsGrounded()
    {
        // check if grounded
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionMask | PickUpcollisionMask);
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

        //If holding a block, drop it
        var drop = gameObject.GetComponent<PlayerPickUpBlock>();
        drop.deathDropObject();

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

        healthAnim.SetBool("IsAlive", true);
        barAnim.SetBool("IsAlive", true);
        playerAnim.SetBool("IsAlive", true);
    }

    //ANIMATION//
    public void Animation()
    {  
        // Player Animation
        if (Input.GetButtonDown("Jump"))
        {
            // enables the jump bool for the jump 1 & 2, disable falling animation entirely
            playerAnim.SetBool("IsJumping", true);
            playerAnim.SetBool("IsFalling", false);
            if (jumpsLeft == 1)
            {
                playerAnim.SetTrigger("triggerJump1");
            }

            if (jumpsLeft == 0)
            {
                playerAnim.SetTrigger("triggerJump2");
            }
        }

        if (IsGrounded() && _rigidbody.velocity.y <= 0)
        {
            // reset the jumping and falling bool, play only the, on ground animation
            playerAnim.SetFloat("Speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
            playerAnim.SetBool("IsJumping", false);
            playerAnim.SetBool("IsFalling", false);
        }

        if (IsGrounded() == false && _rigidbody.velocity.y < -0.25)
        {
            // plays the fall animation when falling 
            playerAnim.SetBool("IsFalling", true);
        }
    }
}
