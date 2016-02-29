using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStamina : MonoBehaviour {

    // Use this for initialization
    public int startingStamina = 100;    // base starting health
    private double staminaPerSec = 1;
    private int currentStamina;
    public Slider staminaSlider;         // Reference to the UI's health bar.



    AttributeManager attributes;
    void Awake()
    {
        attributes = GetComponent<AttributeManager>();
        startingStamina += attributes.getAgility() * 10;   //Mulitply Brawn attribute by 10 to determine starting health. Add to starting health.
        staminaPerSec += (double)attributes.getIntellect() * 0.1;
        staminaSlider.maxValue = startingStamina;
        staminaSlider.value = startingStamina;
        currentStamina = startingStamina;
    }

    void Start()
    {
        StartCoroutine(staminaRegen());
    }
    public void reduceStamina(int amount)
    {
        currentStamina -= amount;
        staminaSlider.value = currentStamina;
    }
    IEnumerator staminaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05F);
            if (currentStamina < startingStamina)
            {
                //TODO FIX THIS CASTING. This will round stamina per sec if it's a decimal.
                currentStamina += (int) staminaPerSec;
                staminaSlider.value = currentStamina;
            }
        }

    }

    public int getCurrentStamina()
    {
        return currentStamina;
    }


}
