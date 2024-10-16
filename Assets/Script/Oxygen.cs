using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Oxygen : MonoBehaviour
{
    public float maxOxygen = 100f; // Max Oxygen
    public float currentOxygen; // Current Oxygen
    public float consumptionRate = 1f; // Oxygen cost every second
    public Player player;

    private TextMeshProUGUI oxygenText; 

    private void Start()
    {
        currentOxygen = maxOxygen; // Initial Oxygen as full
        player = GetComponent<Player>();

        oxygenText = GameObject.Find("OxygenText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        ConsumeOxygen();

        UpdateOxygenUI();

        // Check if Oxygen runs out
        if (currentOxygen <= 0)
        {
            if (player != null)
            {
                player.Death();
            }
            else
            {
                Debug.LogError("Player reference not set in OxygenBar script.");
            }
        }
    }

    void ConsumeOxygen()
    {
        // Reduce oxygen every frame
        currentOxygen -= consumptionRate * Time.deltaTime;
        currentOxygen = Mathf.Max(currentOxygen, 0); // Ensure oxygen doesn't go below 0
    }

    public void AddOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Min(currentOxygen, maxOxygen);
    }

    public void ReduceOxygen(float amount)
    {
        currentOxygen -= amount;
        currentOxygen = Mathf.Max(currentOxygen, 0);
    }

    public void ConsumeOxygenForDash(float amount)
    {
        // Dash costs oxygen
        currentOxygen -= amount;
        currentOxygen = Mathf.Max(currentOxygen, 0);
    }

    public void ResetOxygen()
    {
        currentOxygen = maxOxygen;
    }

    //Temporary Oxygen UI
    private void UpdateOxygenUI()
    {

        if (oxygenText != null)
        {

            oxygenText.text = "Oxygen: " + Mathf.RoundToInt(currentOxygen).ToString();
        }
    }
}
