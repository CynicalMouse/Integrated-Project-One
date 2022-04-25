using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Script : MonoBehaviour
{
    // reference image of the healthbar 
    private Image HealthBar;

    // uses current and max health to find the percentage for the healthbar slider
    public float CurrentHealth;
    private float MaxHealth = 5f;

    PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }
}
