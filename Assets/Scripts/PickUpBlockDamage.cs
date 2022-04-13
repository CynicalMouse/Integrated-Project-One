using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBlockDamage : MonoBehaviour
{
    // How much damage is dealt
    [SerializeField]
    public int damage;


    private void OnCollisionEnter2D(Collision2D other)
    {
        //asign collided object to target varaible 
        var target = other.collider.GetComponent<Enemy>();
        if (target != null)
        {
            //deasl damage
            target.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

}
