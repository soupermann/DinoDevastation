using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    // Define the buttons
    public Button throwAsteroidButton;
    public Button runButton;

    // Define the turn controller
    public TurnController turnController;
   

    // Start is called before the first frame update
    void Start()
    {
        // Add event listeners to the buttons
        throwAsteroidButton.onClick.AddListener(ThrowAsteroid);
        runButton.onClick.AddListener(Run);
    }

    // Update is called once per frame
    void Update()
    {
        // Disable the menu if it's not the current player's turn
        gameObject.SetActive(turnController.currentPlayer == 0 || turnController.currentPlayer == 2);
    }

    // Handle the Throw Asteroid button click event
    void ThrowAsteroid()
    {
        // Set the current player's action to "throw asteroid"
        turnController.selectedAsteroid = true;
    }

    // Handle the Run button click event
    void Run()
    {
        // Set the current player's action to "run"
        turnController.selectedRun = true;
    }
}
