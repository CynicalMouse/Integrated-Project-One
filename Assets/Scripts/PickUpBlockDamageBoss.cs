using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBlockDamageBoss : MonoBehaviour
{
    // How much damage is dealt
    [SerializeField]
    public int damage;
    
    private float speed;

    public Animator whiteCellAnim;

    void Start()
    {
        StartCoroutine(CalcSpeed());
    }

    IEnumerator CalcSpeed()
    {
        bool isPlaying = true;

        while (isPlaying)
        {
            Vector3 prevPos = transform.localPosition;

            yield return new WaitForFixedUpdate();

            speed = Mathf.RoundToInt(Vector3.Distance(transform.localPosition, prevPos) / Time.fixedDeltaTime);

            if (speed > 0)
            {
            whiteCellAnim.SetFloat("Speed", speed);
            }

            if (speed == 0)
            {
            whiteCellAnim.SetFloat("Speed", speed);
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //asign collided object to target varaible 
        var target = other.collider.GetComponent<EnemyBoss>();
        if (target != null)
        {
            //deals damage
            target.GetComponent<EnemyBoss>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }

   
}
