using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaScript : MonoBehaviour
{
    public HealthManager healthManager;
    public HealthManager healthManager0;
    public HealthManager healthManager1;
    public HealthManager healthManager2;

    public static string instance;

    private void Update()
    {
        // Make sure game ends when dinos die
        if (healthManager.healthAmount + healthManager0.healthAmount <= 0)
        {
            instance = "Red";
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
        }
        // Make sure game ends when dinos die
        if (healthManager1.healthAmount + healthManager2.healthAmount <= 0)
        {
            instance = "Blue";
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
        }
    }
    // Make dinos die when collided
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BDL"))
        {
            Die(collision.gameObject, healthManager0);
        }

        if (collision.gameObject.CompareTag("BDR"))
        {
            Die(collision.gameObject, healthManager);

        }
        if (collision.gameObject.CompareTag("RDL"))
        {
            Die(collision.gameObject, healthManager2);

        }
        if (collision.gameObject.CompareTag("RDR"))
        {
            Die(collision.gameObject, healthManager1);

        }

    }

    // Die function so player is out of the game.
    void Die(GameObject dieObject, HealthManager health)
    {
        dieObject.SetActive(false);
        health.healthAmount = 0; // Make sure player is counted as dead for game over animation
    }

}
