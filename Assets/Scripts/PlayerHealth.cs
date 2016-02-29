using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    // Use this for initialization
    public float startingHealth = 100f;    // base starting health
    private float currentHealth;
    public Slider healthSlider;         // Reference to the UI's health bar.
    public Image damageImage;           // Reference to the UI's damage overlay that will flash on damage taken
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    private bool damaged = false;
    private bool isDead = false;
    private bool isDot = false;

    private float damageReduction = 0.0F;

    AttributeManager attributes;
    void Awake () {
        attributes = GetComponent<AttributeManager>();
        startingHealth += attributes.getBrawn();   //Mulitply Brawn attribute by 10 to determine starting health. Add to starting health.
        healthSlider.maxValue = startingHealth;
        currentHealth = startingHealth;
        healthSlider.value = startingHealth;
        damageReduction = attributes.getBrawn() * .01F;
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
        if(!isDead){
          currentHealth -= (amount - (amount * damageReduction));
        }
        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;
        // If player is dead
        if (currentHealth <= 0 && !isDead)
        {
          isDead = true;
          currentHealth = startingHealth;
          healthSlider.value = currentHealth;
          LevelMangerScript.levelManager.RespawnPlayer();
            // ... it should die.
        }
    }
    public void TakeDotDamage(int amount)
    {
      if(!isDot)
      {
        StartCoroutine(DamageOverTime(amount));
      }
      isDot = true;
    }

    public void IsNotDot(){
      isDot = false;
    }

    IEnumerator DamageOverTime(int amount)
    {
      isDot = true;
        while(isDot)
        {
          currentHealth -= (amount - (amount * damageReduction));
          healthSlider.value = currentHealth;
          damaged = true;
          yield return new WaitForSeconds(0.5f);
          if (currentHealth <= 0 && !isDead)
          {
            isDead = true;
            currentHealth = startingHealth;
            healthSlider.value = currentHealth;
            LevelMangerScript.levelManager.RespawnPlayer();
              // ... it should die.
          }
        }
    }
    public void isNotDead(){
      isDead = false;
    }

}
