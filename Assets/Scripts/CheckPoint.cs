using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Animator objectiveList;
    public Animator mapImage;
    public Animator mapName;

    public float cpNumber;

    public GameObject checkMark;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if Player is triggering collider
        if (other.CompareTag("Player"))
        {
            objectiveList.SetFloat("Checkpoint", cpNumber);
            mapImage.SetFloat("Checkpoint", cpNumber);
            mapName.SetFloat("Checkpoint", cpNumber);

            checkMark.SetActive(true);

            var player = other.GetComponent<PlayerMovement>();
            player.SetRespawnPoint(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
