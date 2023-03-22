using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    // Define the camera
    public Camera mainCamera;
    public Text timerText;

    // Define the teams
    public GameObject[] players;
    public GameObject turnArrow;
    public Transform throwableObject;
    public GameObject asteroidBDL;
    public GameObject asteroidBDR;
    public GameObject asteroidRDR;
    public GameObject asteroidRDL;
    Vector3 originalPosBDL;
    Vector3 originalPosBDR;
    Vector3 originalPosRDR;
    Vector3 originalPosRDL;


    // Buttons clicked
    public bool asteroidButton = false;
    public bool moveButton = false;

    // Respawn force
    public float respawnForce = 10f;

    // Keep track of time
    public int currentPlayer = 0;
    public float turnStartTime = 0f;
    public float timeLeft;

    public bool BLturn = false;
    public bool BRturn = false;
    public bool RLturn = false;
    public bool RRturn = false;

    public bool deadNext = false;// Keep track of dead 

    public bool selectedWall = false;
    public bool selectedRun = false;
    public bool selectedAsteroid = false;
    void Awake()
    {
        // Storing original positions for respawning
        originalPosBDL = asteroidBDL.transform.position;
        originalPosBDR = asteroidBDR.transform.position;
        originalPosRDR = asteroidRDR.transform.position;
        originalPosRDL = asteroidRDL.transform.position;

        // Set moving and asteroids to false to start the turn
        checkActiveThrow();

        checkActiveMove();

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

        // Move to next turn when dead. 
        if (!players[currentPlayer].activeSelf)
        {
            deadNext = true;
        }

        // Check if the current player's turn is over
        if (Input.GetKeyDown(KeyCode.P) || Time.time - turnStartTime >= turnTimeLimit || deadNext)
        {
            if (currentPlayer == 0 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {

                // Reset asteroid
                resetAsteroid(asteroidBDL,originalPosBDL);

                checkActiveThrow();

                // Turn setter
                BLturn = true;
                BRturn = false;
                RLturn = false;
                RRturn = false;

                // Moving
                checkActiveMove();
            }
            else if (currentPlayer == 1 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {

                // Asteroids
                resetAsteroid(asteroidRDR,originalPosRDR);
                checkActiveThrow();

                // Turn setter
                BLturn = false;
                BRturn = false;
                RLturn = false;
                RRturn = true;

                // Moving
                checkActiveMove();

            }
            else if (currentPlayer == 2 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
                // Asteroids
                resetAsteroid(asteroidBDR,originalPosBDR);

                checkActiveThrow();

                // Turn setter
                BLturn = false;
                BRturn = true;
                RLturn = false;
                RRturn = false;

                // Moving
                checkActiveMove();

            }
            else if (currentPlayer == 3 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {


                // Asteroids
                resetAsteroid(asteroidRDL,originalPosRDL);

                checkActiveThrow();

                // Turn setter
                BLturn = false;
                BRturn = false;
                RLturn = true;
                RRturn = false;

                // Moving
                checkActiveMove();

            }

            // Reset the turn start time
            currentPlayer++;

            if (currentPlayer == 4) { currentPlayer = 0; }
            turnStartTime = Time.time;
            // Reset buttons
            asteroidButton = false;
            moveButton = false;
            deadNext = false; // Reset value
        }
    }
    void FixedUpdate()
    {
        // Timer
        timeLeft = turnTimeLimit - (Time.time - turnStartTime);
        timerText.text = " " + timeLeft.ToString("F2");
        if (turnTimeLimit - (Time.time - turnStartTime) < 5) {
            timerText.color = Color.red;
        }
        else {
            timerText.color = Color.green;
        }

        // Green arrow for players turn
        turnArrow.transform.position = new Vector3(players[currentPlayer].transform.position.x, players[currentPlayer].transform.position.y + 1.5f, 0f);

    }


   
    // Functions to select actions
    public void turnOnAsteroid()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.SetActive(true);
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
        if (currentPlayer == 2)
        {
            asteroidBDR.SetActive(true);
            asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
    }

    public void turnOnAsteroidRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.SetActive(true);
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
        if (currentPlayer == 3)
        {
            asteroidRDL.SetActive(true);
            asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
    }

    public void turnOnMove()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.SetActive(false);
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
        if (currentPlayer == 2)
        {
            asteroidBDR.SetActive(false);
            asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
    }

    public void turnOnMoveRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.SetActive(false);
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
        if (currentPlayer == 3)
        {
            asteroidRDL.SetActive(false);
            asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
    }

    public void checkActiveThrow()
    {
        // Only call objects if character is active in game
        if (players[1].activeSelf)
        {
            asteroidRDR.SetActive(false);
        }
        if (players[2].activeSelf)
        {
            asteroidBDR.SetActive(false);
        }
        if (players[0].activeSelf)
        {
            asteroidBDL.SetActive(false);
        }
        if (players[3].activeSelf)
        {
            asteroidRDL.SetActive(false);
        }
    }

    public void checkActiveMove()
    {
        // Only call objects if character is active in game
        if (players[0].activeSelf)
        {
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
        if (players[2].activeSelf)
        {
            asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
        if (players[1].activeSelf)
        {
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        }
        if (players[3].activeSelf)
        {
            asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        }

    }

    public void resetAsteroid(GameObject asteroid,Vector3 position)
    {
        // Reset asteroid
        asteroid.transform.position = position;
        // Making the asteroid not fall
        asteroid.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        asteroid.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        asteroid.GetComponent<Rigidbody2D>().gravityScale = 0f;
        asteroid.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.
    }


}
