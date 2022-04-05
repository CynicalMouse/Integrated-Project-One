using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Making Variables

    //Handle Cooldowns
    //Cool down current length 
    private float AttackCooldown;

    //Cool down total length
    [SerializeField]
    public float AttackCooldownStart;

    //Attack Values

    // Where the attack comes from
    public Transform AttackPos;

    // The range of the attack
    [SerializeField]
    public float AttackRange;

    // How much damage is dealt
    [SerializeField]
    public int damage;

    // making sure it is an enemy and not the enviroment
    public LayerMask IsEnemy;


    void Update()
    {
        //Check if can attack
        if (AttackCooldown <= 0)
        {
            if (Input.GetKey(KeyCode.C))
            {
                // check for all objects in the Enemy layer and adds them to an array
                Collider2D[] damageableEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, IsEnemy);

                // Damages all enemies in damageableEnemies array
                for (int i = 0; i < damageableEnemies.Length; i++)
                {
                    damageableEnemies[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }

            //Starts Cooldown
            AttackCooldown = AttackCooldownStart;
        }
        else
        {
            // Counting down cool down
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Displays a circle in editor for how long attack range is
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPos.position, AttackRange);
    }
}
