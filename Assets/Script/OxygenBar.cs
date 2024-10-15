using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Image oxygenBarImage; // Get UI compoment
    public float maxOxygen = 100f; // Max Oxygen
    public float currentOxygen; // Current Oxygen
    public float consumptionRate = 1f; // Oxygen cost every second
    public Player player;

    private void Start()
    {
        currentOxygen = maxOxygen; //Inital Oxygen as full
        oxygenBarImage.fillAmount = 1f; 
    }

    private void Update()
    {
        ConsumeOxygen(); 
        UpdateOxygenBar();

        //check is Oxygen run out
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
        // every fram cost Oxygen
        currentOxygen -= consumptionRate * Time.deltaTime;
        currentOxygen = Mathf.Max(currentOxygen, 0); // make sure Oxygen will not go lower then 0
    }

    void UpdateOxygenBar()
    {
        
        oxygenBarImage.fillAmount = currentOxygen / maxOxygen;
    }

    public void AddOxygen(float amount)
    {
        currentOxygen += amount;
        currentOxygen = Mathf.Min(currentOxygen, maxOxygen);
        UpdateOxygenBar();
    }

    public void ReduceOxygen(float amount)
    {
        currentOxygen -= amount;
        currentOxygen = Mathf.Max(currentOxygen, 0);
        UpdateOxygenBar();
    }
    public void ConsumeOxygenForDash(float amount)
    {
        //dash cost Oxygen
        currentOxygen -= amount;
        currentOxygen = Mathf.Max(currentOxygen, 0);
        UpdateOxygenBar();
    }
    public void ResetOxygen()
    {
        currentOxygen = maxOxygen;
        UpdateOxygenBar(); 
    }
}