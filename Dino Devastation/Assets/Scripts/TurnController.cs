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
    public Transform throwableObject;
    public GameObject asteroidBDL;
    public GameObject asteroidBDR;
    public GameObject asteroidRDR;
    public GameObject asteroidRDL;
    Vector3 originalPos;
    Vector3 originalPos0;
    Vector3 originalPos1;
    Vector3 originalPos2;


    // Buttons clicked
    public bool asteroidButton = false;
    public bool moveButton = false;


    // Keep track of time
    public int currentPlayer = 0;
    public float turnStartTime = 0f;

    public bool BLturn = false;
    public bool BRturn = false;
    public bool RLturn = false;
    public bool RRturn = false;

    public bool selectedWall = false;
    public bool selectedRun = false;
    public bool selectedAsteroid = false;
    void Awake()
    {
        // Storing original positions for respawning
        originalPos = asteroidBDL.transform.position;
        originalPos0 = asteroidBDR.transform.position;
        originalPos1 = asteroidRDR.transform.position;
        originalPos2 = asteroidRDL.transform.position;

        // Set moving and asteroids to false to start the turn
        asteroidBDL.SetActive(false);
        asteroidBDR.SetActive(false);
        asteroidRDR.SetActive(false);
        asteroidRDL.SetActive(false);

        asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
        asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
        asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;

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
                
                // Asteroids 
                asteroidRDR.transform.position = originalPos1;

                asteroidRDR.SetActive(false);
                asteroidBDR.SetActive(false);
                asteroidBDL.SetActive(false);
                asteroidRDL.SetActive(false);
                BLturn = true;
                BRturn = false;
                RLturn = false;
                RRturn = false;
                //asteroid.SetActive(true);

                // Moving
                asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;

            }
            else if (currentPlayer == 1 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
               
                // Asteroids
                asteroidBDR.transform.position = originalPos0;
                BLturn = false;
                BRturn = false;
                RLturn = false;
                RRturn = true;
                asteroidBDL.SetActive(false);

                asteroidRDR.SetActive(false);
                asteroidRDL.SetActive(false);
                asteroidBDR.SetActive(false);
                //asteroid.SetActive(true);

                // Moving
                asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;
            }
            else if (currentPlayer == 2 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {
                // Asteroids
                asteroidRDL.transform.position = originalPos2;
                asteroidRDR.SetActive(false);
                asteroidBDR.SetActive(false);
                asteroidBDL.SetActive(false);
                asteroidRDL.SetActive(false);
                BLturn = false;
                BRturn = true;
                RLturn = false;
                RRturn = false;
                //asteroid2.SetActive(true);

                // Moving
                asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;
            }
            else if (currentPlayer == 3 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
            {

              
                // Asteroids
                asteroidBDL.transform.position = originalPos;
                asteroidRDL.SetActive(false);
                asteroidRDR.SetActive(false);
                asteroidBDR.SetActive(false);
                asteroidBDL.SetActive(false);
                BLturn = false;
                BRturn = false;
                RLturn = true;
                RRturn = false;
                //asteroid1.SetActive(true);

                // Moving
                asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = false;
            }

            // Reset the turn start time
            currentPlayer++;
            if (currentPlayer == 4) { currentPlayer = 0; }
            turnStartTime = Time.time;
            // Reset buttons
            asteroidButton = false;
            moveButton = false;
        }
    }
    void FixedUpdate()
    {
        turnArrow.transform.position = new Vector3(players[currentPlayer].transform.position.x, players[currentPlayer].transform.position.y + 1.5f, 0f);
    }

    // Functions to select actions
    public void turnOnAsteroid()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.SetActive(true);
        }
        if (currentPlayer == 2)
        {
            asteroidBDR.SetActive(true);
        }
    }

    public void turnOnAsteroidRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.SetActive(true);
        }
        if (currentPlayer == 3)
        {
            asteroidRDL.SetActive(true);
        }
    }

    public void turnOnMove()
    {
        if(currentPlayer == 0)
        {
            asteroidBDL.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
        if (currentPlayer == 2)
        {
            asteroidBDR.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
    }

    public void turnOnMoveRed()
    {
        if (currentPlayer == 1)
        {
            asteroidRDR.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
        if (currentPlayer == 3)
        {
            asteroidRDL.GetComponentInParent<BlueDino1Script>().enabled = true;
        }
    }
}
