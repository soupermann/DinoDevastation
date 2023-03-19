using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    // Define the camera
    public Camera mainCamera;

    // Define the teams
    public GameObject[] players;
    public GameObject turnArrow;
    
    public int currentPlayer = 0;
    public float turnStartTime = 0f;

    public bool BLturn = false;
    public bool BRturn = false;
    public bool RLturn = false;
    public bool RRturn = false;
    
    private bool selectedWall = false;
    private bool selectedRun = false;
    private bool selectedAsteroid= false;

    
    // Define the turn time limit
    public float turnTimeLimit = 5f; // in seconds
    
    // Start is called before the first frame update
    void Start()
    {
        // Initialize the turn variables
        currentPlayer = 0;
        turnStartTime = Time.time;
        // Set the camera to focus on the first player's sprite
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current player's turn is over
        if (Input.GetKeyDown(KeyCode.P) || Time.time - turnStartTime >= turnTimeLimit)
        {
            if (currentPlayer == 0 && players[currentPlayer].activeInHierarchy)
                {
                    BLturn = true;
                    BRturn = false;
                    RLturn = false;
                    RRturn = false;
                }
            else if (currentPlayer == 1 && players[currentPlayer].activeInHierarchy)
                {
                    BLturn = false;
                    BRturn = false;
                    RLturn = false;
                    RRturn = true;
                }
            else if (currentPlayer == 2 && players[currentPlayer].activeInHierarchy)
                {
                    BLturn = false;
                    BRturn = true;
                    RLturn = false;
                    RRturn = false;
                }
            else if (currentPlayer == 3 && players[currentPlayer].activeInHierarchy)
                {
                    
                    BLturn = false;
                    BRturn = false;
                    RLturn = true;
                    RRturn = false;
                }
            // Reset the turn start time

            currentPlayer++;
            if (currentPlayer == 4) {currentPlayer = 0;}
            turnStartTime = Time.time;
        }
    }
    void FixedUpdate() {
        turnArrow.transform.position = new Vector3(players[currentPlayer].transform.position.x, players[currentPlayer].transform.position.y + 1.5f,0f);
    }
}
