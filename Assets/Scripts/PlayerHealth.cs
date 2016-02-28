using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization
    public int startingHealth = 100;    // base starting health
    private int currentHealth;
    public Slider healthSlider;         // Reference to the UI's health bar.
    public Image damageImage;           // Reference to the UI's damage overlay that will flash on damage taken
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    private bool damaged = false;
    private bool isDead = false;

    AttributeManager attributes;
    void Awake () {
        attributes = GetComponent<AttributeManager>();
        startingHealth += attributes.getBrawn() * 10;   //Mulitply Brawn attribute by 10 to determine starting health. Add to starting health.
        healthSlider.maxValue = startingHealth;
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (damaged)
        {
            // ... set the color of the damageImage to the flash color.
            damageImage.color = flashColor;
        }
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }
    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;
        // Reduce the current health by the damage amount.
        currentHealth -= amount;
        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        // If player is dead
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
        }
    }


}

