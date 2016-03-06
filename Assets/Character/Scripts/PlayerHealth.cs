using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthbar;
    private float currentHealth, maxHealth;
    private bool invulnerable = false;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth = 100;//starting health is 100
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns the percentage of health remaining
    private float getHealthRemaining()
    {
        return ((currentHealth / maxHealth) * 100);
    }

    //Returns the player to vulnerable status so they can take damage
    private void makeVulnerable()
    {
        Debug.Log("Player is vulnerable");
        invulnerable = false;
    }

    //Reduces health based on damage taken
    //@param damage - damage taken
    public void takeDamage(float damage)
    {
        if (!invulnerable)
        {
            Debug.Log("Player took damage");
            Debug.Log("Player is invulnerable");
            invulnerable = true;
            currentHealth -= damage;
            //health can't be a negative number
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
            healthbar.value = getHealthRemaining();//adjust the healthbar display now that health has been changed
            Invoke("makeVulnerable", 1.5f);
        }
    }

    
}
