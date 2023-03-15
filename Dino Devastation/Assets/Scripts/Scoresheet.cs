using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scoresheet : MonoBehaviour {


    private Text _scoreText;
    private Text TeamWinsText;
    private Text reddamage;
    private Text bluedamage;
    private string _currentSceneName;
    private void Start() {
        TeamWinsText = transform.Find("Team Wins").GetComponent<Text>();
        reddamage = transform.Find("Red Damage").GetComponent<Text>();
        bluedamage = transform.Find("Blue Damage").GetComponent<Text>();
        _currentSceneName = SceneManager.GetActiveScene().name;
        if (SceneManager.GetActiveScene().name == "Scoresheet")
        {
        TeamWinsText.text = "Team " + Asteroid.instance + " Wins!";
        reddamage.text = "Team Red Did " + Asteroid.counter + " Damage";
        bluedamage.text = "Team Blue Did " + Asteroid.counter2 + " Damage";
        Show();
        } 
        else{
        
        Hide();
        }
        }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    public void RetryClicked() {
        Asteroid.counter2 = 0;
        Asteroid.counter = 0;
        SceneManager.LoadScene(PlayerPrefs.GetInt ("lastLevel"));
    }
    public void ExitClicked() {
        Asteroid.counter2 = 0;
        Asteroid.counter = 0;
        SceneManager.LoadScene("Initial Scene", LoadSceneMode.Single);
    }
}