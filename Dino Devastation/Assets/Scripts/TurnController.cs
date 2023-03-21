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
               
                // Asteroids

                // Reset asteroid
                asteroidBDL.transform.position = originalPosBDL;
                // Making the asteroid not fall
                asteroidBDL.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                asteroidBDL.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                asteroidBDL.GetComponent<Rigidbody2D>().gravityScale = 0f;

                asteroidBDL.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.


                checkActiveThrow();

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

                // Reset asteroid
                asteroidRDR.transform.position = originalPosRDR;
                // Making the asteroid not fall
                asteroidRDR.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                asteroidRDR.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                asteroidRDR.GetComponent<Rigidbody2D>().gravityScale = 0f;

                asteroidRDR.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.

                BLturn = false;
                BRturn = false;
                RLturn = false;
                RRturn = true;
                checkActiveThrow();

                // Moving
                checkActiveMove();

            }
            else if (currentPlayer == 2 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
                // Asteroids

                // Reset asteroid
                asteroidBDR.transform.position = originalPosBDR;
                asteroidBDR.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.
                // Making the asteroid not fall
                asteroidBDR.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                asteroidBDR.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                asteroidBDR.GetComponent<Rigidbody2D>().gravityScale = 0f;

                checkActiveThrow();

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

                // Reset asteroid
                asteroidRDL.transform.position = originalPosRDL;
                asteroidRDL.GetComponent<ThrowingScript>().hasBeenThrown = false; // Reset the has been thrown bool to make it able to be thrown again.
                // Making the asteroid not fall
                asteroidRDL.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                asteroidRDL.GetComponent<Rigidbody2D>().angularVelocity = 0f;
                asteroidRDL.GetComponent<Rigidbody2D>().gravityScale = 0f;

                checkActiveThrow();

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
        timeLeft = turnTimeLimit - (Time.time - turnStartTime);
        timerText.text = " " + timeLeft.ToString("F2");
        if (turnTimeLimit - (Time.time - turnStartTime) < 5) {
            timerText.color = Color.red;
        }
        else {
            timerText.color = Color.green;
        }
        
        // Deal with deaths, should use teammates turn as their turn.
        //if (!players[currentPlayer].activeSelf)
        //{
        //    teammateSwitch(players[currentPlayer]);
        //    Debug.Log("GOT IN");
        //}
        //else
        //{
        turnArrow.transform.position = new Vector3(players[currentPlayer].transform.position.x, players[currentPlayer].transform.position.y + 1.5f, 0f);
        //}

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

    public void teammateSwitch(GameObject teammate)
    {
        if (players[currentPlayer] == players[0])
        {
            currentPlayer = 1;
            teammate = players[2];
            Debug.Log("GO IN HERE TOO");
        }
        if (players[currentPlayer] == players[1])
        {
            currentPlayer = 3;
            teammate = players[3];
        }
        if (players[currentPlayer] == players[2])
        {
            currentPlayer = 0;
            teammate = players[0];
        }
        if (players[currentPlayer] == players[3])
        {
            currentPlayer = 1;
            teammate = players[1];
        }
        turnArrow.transform.position = new Vector3(teammate.transform.position.x, teammate.transform.position.y + 1.5f, 0f);
    }

   
}
