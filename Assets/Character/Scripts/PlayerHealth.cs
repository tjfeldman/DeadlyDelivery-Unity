using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthbar;
    private float currentHealth, maxHealth;

    // Use this for initialization
    void Awake()
    {
        currentHealth = maxHealth = 100;//starting health is 100
        takeDamage(15);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Reduces health based on damage taken
    //@param damage - damage taken
    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        //health can't be a negative number
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthbar.value = getHealthRemaining();//adjust the healthbar display now that health has been changed
    }

    //Returns the percentage of health remaining
    private float getHealthRemaining()
    {
        return ((currentHealth / maxHealth) * 100);
    }
}
