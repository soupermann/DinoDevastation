using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
public class TurnController : MonoBehaviour
{
    // Define the camera
    public Camera mainCamera;

    // Define the teams
    public GameObject[] team1;
    public GameObject[] team2;
    
    private int currentPlayer = 0;
    private int currentTeam = 1;
    private float turnStartTime = 0f;
    
    // Define the turn time limit
    public float turnTimeLimit = 20f; // in seconds
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the turn variables
        currentPlayer = 0;
        currentTeam = 1;
        turnStartTime = Time.time;
        // Set the camera to focus on the first player's sprite
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current player's turn is over
        if (Input.GetKeyDown(KeyCode.Space) || Time.time - turnStartTime >= turnTimeLimit)
        {
            // Switch to the next player
            currentPlayer++;
            
            // Check if the current team's turn is over
            if (currentPlayer >= team1.Length && currentTeam == 1)
            {
                currentTeam = 2;
                currentPlayer = 0;
            }
            else if (currentPlayer >= team2.Length && currentTeam == 2)
            {
                currentTeam = 1;
                currentPlayer = 0;
            }
            
            // Set the camera to focus on the new player's sprite
            if (currentTeam == 1)
            {
                
            }
            else if (currentTeam == 2)
            {
                
            }
            
            // Reset the turn start time
            turnStartTime = Time.time;
        }
    }
}
