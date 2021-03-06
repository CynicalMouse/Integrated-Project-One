using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    //Define Variables
    // Serialize Field makes vaiables visible in editor

    // Attributes
    [SerializeField]
    public int health;

    [SerializeField]
    public float speed;

    //Movement

    // store points
    [SerializeField]
    private Transform[] _points;

    // Distance at which the platform is considered to have reached a point
    [SerializeField]
    private float checkDistance = 0.05f;

    // Variables for which point the platform is moving to
    private Transform targetPoint;
    private int targetPointIndex = 0;

    // Used to flip sprite 
    private SpriteRenderer spr;

    // Used as final congratulations on winning
    public Animator bossD;
    public Animator screenWin;
    public GameObject toggleWin;
    public GameObject togglePause;

    void Start()
    {
        // target first point
        targetPoint = _points[0];

        spr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(health <= 0)
        {
            bossD.SetTrigger("triggerDeath");
            screenWin.SetTrigger("triggerWin");
            toggleWin.SetActive(true);
            togglePause.SetActive(false);
        }

        //MOVEMENT

        // move towards the current target point
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        // when target point is reached, move to next one
        if (Vector2.Distance(transform.position, targetPoint.position) < checkDistance)
        {
            targetPoint = GetNextPoint();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("damage taken");
    }

    private Transform GetNextPoint()
    {
        // increase by one to target next point
        targetPointIndex++;

        // if number of points exceeded, go back to first point
        if (targetPointIndex >= _points.Length)
        {
            targetPointIndex = 0;
        }

        return _points[targetPointIndex];
    }
}

