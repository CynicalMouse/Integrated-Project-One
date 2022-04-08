using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooter : MonoBehaviour
{

    // range to shoot at player
    [SerializeField]
    public float range;

    // how fast the bullet travels
    [SerializeField]
    public float speed;

    // time between shots
    [SerializeField]
    public float timeBetweenShots;

    // current distance from player
    private float playerDistance;

    // players location
    [SerializeField]
    public Transform player;

    // shoot position
    [SerializeField]
    public Transform shootPos;

    // store the projectile
    [SerializeField]
    public GameObject projectile;

    // if it can shoot
    private bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        //get distance between enemy and player
        playerDistance = Vector2.Distance(transform.position, player.position);

        if(playerDistance <= range)
        {
            if (canShoot == true)
            { 
                StartCoroutine(ShootProjectile());
            }
        }
    }

    IEnumerator ShootProjectile()
    {
        // stop it from shooting again
        canShoot = false;

        // wait to shoot
        yield return new WaitForSeconds(timeBetweenShots);

        // create bullet
        GameObject newProjectile = Instantiate(projectile, shootPos.position, Quaternion.identity);

        //Activate projectile Clone
        newProjectile.gameObject.SetActive(true);

        //newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(speed *Time.fixedDeltaTime, 0f);

        // let it shoot again
        canShoot = true;

        Debug.Log("shoot");
    }
}
