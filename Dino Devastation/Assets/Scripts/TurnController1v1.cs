using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController1v1 : MonoBehaviour
{
    // Define the camera
    public Camera mainCamera;
    public Text timerText;
    // Define the teams
    public GameObject[] players;
    public GameObject turnArrow;
    public Transform throwableObject;
    public GameObject asteroidBDL;
    
    public GameObject asteroidRDR;
    
    Vector3 originalPosBDL;
    
    Vector3 originalPosRDR;
   


    // Buttons clicked
    public bool asteroidButton = false;
    public bool moveButton = false;


    // Keep track of time
    public int currentPlayer = 0;
    public float turnStartTime = 0f;
    public float timeLeft;
    public bool BLturn = false;
    public bool RRturn = false;

    public bool selectedWall = false;
    public bool selectedRun = false;
    public bool selectedAsteroid = false;
    void Awake()
    {
        // Storing original positions for respawning
        originalPosBDL = asteroidBDL.transform.position;
       
        originalPosRDR = asteroidRDR.transform.position;
        

        // Set moving and asteroids to false to start the turn
        asteroidBDL.SetActive(false);
        
        asteroidRDR.SetActive(false);
        

        asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        
        asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        

    }


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
        // Make keys activate running and throwing
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turnOnMoveRed();
            turnOnMove();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            turnOnAsteroid();
            turnOnAsteroidRed();
        }
      
        // Check if the current player's turn is over
        if (Input.GetKeyDown(KeyCode.P) || Time.time - turnStartTime >= turnTimeLimit)
        {
            if (currentPlayer == 0 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
                
                asteroidBDL.transform.position = originalPosBDL;
                asteroidBDL.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.

                
                checkActiveThrow();
                BLturn = true;
                
                RRturn = false;

                // Moving
                checkActiveMove();
                

            }
            else if (currentPlayer == 1 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
               
                asteroidRDR.transform.position = originalPosRDR;
                asteroidRDR.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.

                //asteroidBDR.transform.position = originalPosBDR;
                checkActiveThrow();
                BLturn = false;
                
                RRturn = true;
                

                // Moving
               checkActiveMove();
            }
            // Reset the turn start time
            currentPlayer++;
            if (currentPlayer == 2) { currentPlayer = 0; }
            turnStartTime = Time.time;
            // Reset buttons
            asteroidButton = false;
            moveButton = false;
        }
    }
    void FixedUpdate()
    {
        
        timeLeft = turnTimeLimit - (Time.time - turnStartTime);
        timerText.text = " " + timeLeft.ToString("F2");
        if (turnTimeLimit - (Time.time - turnStartTime) < 5) {
            timerText.color = Color.red;
        }
        else {
            timerText.color = Color.green;
        }
        turnArrow.transform.position = new Vector3(players[currentPlayer].transform.position.x, players[currentPlayer].transform.position.y + 1.5f, 0f);

    }

    // Functions to select actions
    public void turnOnAsteroid()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.SetActive(true);
        }
        
    }

    public void turnOnAsteroidRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.SetActive(true);
        }
    
    }

    public void turnOnMove()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = true;
        }

    }

    public void turnOnMoveRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
   
    }
        public void checkActiveMove()
    {
        // Only call objects if character is active in game
        if (players[0].activeSelf)
        {
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        }

        if (players[1].activeSelf)
        {
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
    }
        public void checkActiveThrow()
    {
        // Only call objects if character is active in game
        if (players[1].activeSelf)
        {
            asteroidRDR.SetActive(false);
        }
    
        if (players[0].activeSelf)
        {
            asteroidBDL.SetActive(false);
        }
    
    }
}