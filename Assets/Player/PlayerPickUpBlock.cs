using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpBlock : MonoBehaviour
{
    //Variable for where the box sits on the player (viewable in editor)
    public Transform HoldSpot;

    //Where the player can pickup stuff (viewable in editor)
    public Transform PickUpPos;

    // range of pickup
    [SerializeField]
    public float PickUpRange;

    //getting what layer items can be picked up from
    public LayerMask pickupMask;

    // variable to see if the player is holding object
    private GameObject HeldItem;

    // how strong the throw is
    [SerializeField]
    public float ThrowPower;

    // Where the object will be thrown
    [SerializeField]
    private Transform ThrowDirection;

    public Animator playerCarryAnim;

    void Update()
    {

        //Key Input
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Drop held item
            if(HeldItem)
            {
                DropObject();
            }
            // Pick up item if nothing held
            else
            {
                PickUpObject();
            } 
        }

    }

    void PickUpObject()
    {
        // asign object in range to variable
        Collider2D pickUpItem = Physics2D.OverlapCircle(PickUpPos.position, PickUpRange, pickupMask);

        if (pickUpItem)
        {
            //Asign object to HeldItem variable
            HeldItem = pickUpItem.gameObject;
            //Move object to where it is held on player
            HeldItem.transform.position = HoldSpot.position;
            //Make the player the parent of object
            HeldItem.transform.parent = transform;

            // turn off rigidbody of object so it follows the player correctly
            if (HeldItem.GetComponent<Rigidbody2D>())
                HeldItem.GetComponent<Rigidbody2D>().simulated = false;

            // plays animation of picking up the object 
            playerCarryAnim.SetBool("IsCarry", true);
            playerCarryAnim.SetTrigger("triggerPickup");
        }
    }

    public void DropObject()
    {
        // additional if statement to fix dropping item on death
        if (HeldItem)
        {
            //Place item infront of player
            //HeldItem.transform.position = PickUpPos.position;

            // remove item from player (no longer child of player)
            HeldItem.transform.parent = null;

            //turn back on rigidbody
            if (HeldItem.GetComponent<Rigidbody2D>())
                HeldItem.GetComponent<Rigidbody2D>().simulated = true;



            // Throws item
            HeldItem.GetComponent<Rigidbody2D>().AddForce(ThrowDirection.localPosition * ThrowPower, ForceMode2D.Impulse);

            // rest variable
            HeldItem = null;

            // plays animation of drop/throwing the object 
            playerCarryAnim.SetBool("IsCarry", false);
            playerCarryAnim.SetTrigger("triggerThrow");
        }
        else
        {
            // does nothing
            return;
        }
    }

    public void deathDropObject()
    {
        // additional if statement to fix dropping item on death
        if (HeldItem)
        {
            //Place item infront of player
            HeldItem.transform.position = PickUpPos.position;

            // remove item from player (no longer child of player)
            HeldItem.transform.parent = null;

            //turn back on rigidbody
            if (HeldItem.GetComponent<Rigidbody2D>())
                HeldItem.GetComponent<Rigidbody2D>().simulated = true;

            // rest variable
            HeldItem = null;
        }
        else
        {
            // does nothing
            return;
        }
    }

}
