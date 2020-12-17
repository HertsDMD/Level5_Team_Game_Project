using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour

{
    bool playerDies;

    Transform playerStartPosition;


    int startHealth = 100;
    int currentHealth;
    Slider healthSlider;

    bool isDead = false;
    bool damaged;





    Image damageImage;
    public float flashSpeed = 2f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.3f);




    // Start is called before the first frame update
    void Start()
    {

        playerStartPosition = transform;

        currentHealth = startHealth;
               
      
        healthSlider = GameObject.Find("Slider").GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

        if (damaged)
        {
            damageImage.color = flashColour;
            
        }
        else
        {
            if (damageImage != null)
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);

            }
        }

        damaged = false;

        if (transform.position.y < -10 && !isDead)
        {
            TakeDamage(100);

        }

    }

    
    public void addHealth(int amout)
    {
        currentHealth += amout;

        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
       

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        Debug.Log("player damaged");


        currentHealth -= amount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }


        if (currentHealth <= 0 && !playerDies)
        {

            Death();
            damaged = false;
            Debug.Log("player is dead");

        }

    }


    void Death()
    {
        isDead = true;
        playerDies = true;



    }
}
