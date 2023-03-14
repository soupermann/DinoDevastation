using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(healthAmount <= 0)
        {
            // Remove character
        }
        // TESTING EXAMPLES FOR NOW
        if(Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20,healthBar);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5,healthBar);
        }
    }
    
    public void TakeDamage(float damage,Image healthBar)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount, Image healthBar)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}
