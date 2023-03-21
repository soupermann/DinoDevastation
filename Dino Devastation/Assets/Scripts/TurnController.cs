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
    public GameObject asteroid;
    public GameObject asteroid0;
    public GameObject asteroid1;
    public GameObject asteroid2;
    Vector3 originalPos;
    Vector3 originalPos0;
    Vector3 originalPos1;
    Vector3 originalPos2;

  



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
        originalPos = asteroid.transform.position;
        originalPos0 = asteroid0.transform.position;
        originalPos1 = asteroid1.transform.position;
        originalPos2 = asteroid2.transform.position;

        // Only settig asteroids true if its on their turn
        asteroid.SetActive(true);
        asteroid0.SetActive(false);
        asteroid1.SetActive(false);
        asteroid2.SetActive(false);

        // For moving
        asteroid.GetComponentInParent<BlueDino1Script>().enabled = true;
        asteroid0.GetComponentInParent<BlueDino1Script>().enabled = false;
        asteroid1.GetComponentInParent<BlueDino1Script>().enabled = false;
        asteroid2.GetComponentInParent<BlueDino1Script>().enabled = false;

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
        // Check if the current player's turn is over
        if (Input.GetKeyDown(KeyCode.P) || Time.time - turnStartTime >= turnTimeLimit)
        {
            if (currentPlayer == 0 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
               {
                // Asteroids 
                    asteroid1.transform.position = originalPos1;
                    asteroid0.SetActive(false);
                    asteroid.SetActive(false);
                    asteroid2.SetActive(false);
                    asteroid1.SetActive(true);
                    BLturn = true;
                    BRturn = false;
                    RLturn = false;
                    RRturn = false;
                //asteroid.SetActive(true);

                // Moving
                asteroid.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid0.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid1.GetComponentInParent<BlueDino1Script>().enabled = true;
                asteroid2.GetComponentInParent<BlueDino1Script>().enabled = false;

            }
            else if (currentPlayer == 1 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
                {
                // Asteroids
                    asteroid0.transform.position = originalPos0; 
                    BLturn = false;
                    BRturn = false;
                    RLturn = false;
                    RRturn = true;
                    asteroid.SetActive(false);
                    
                    asteroid1.SetActive(false);
                    asteroid2.SetActive(false);
                    asteroid0.SetActive(true);
                //asteroid.SetActive(true);

                // Moving
                asteroid.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid0.GetComponentInParent<BlueDino1Script>().enabled = true;
                asteroid1.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid2.GetComponentInParent<BlueDino1Script>().enabled = false;
            }
            else if (currentPlayer == 2 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
                {
                // Asteroids
                    asteroid2.transform.position = originalPos2;
                    asteroid1.SetActive(false);
                    asteroid0.SetActive(false);
                    asteroid.SetActive(false);
                    asteroid2.SetActive(true);
                    BLturn = false;
                    BRturn = true;
                    RLturn = false;
                    RRturn = false;
                //asteroid2.SetActive(true);

                // Moving
                asteroid.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid0.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid1.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid2.GetComponentInParent<BlueDino1Script>().enabled = true;
            }
            else if (currentPlayer == 3 && players[currentPlayer].activeInHierarchy && players[currentPlayer] != null && players[currentPlayer].activeSelf)
                {
                // Asteroids
                    asteroid.transform.position = originalPos;
                    asteroid2.SetActive(false);
                    asteroid1.SetActive(false);
                    asteroid0.SetActive(false);
                    asteroid.SetActive(true);
                    BLturn = false;
                    BRturn = false;
                    RLturn = true;
                    RRturn = false;
                //asteroid1.SetActive(true);

                // Moving
                asteroid.GetComponentInParent<BlueDino1Script>().enabled = true;
                asteroid0.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid1.GetComponentInParent<BlueDino1Script>().enabled = false;
                asteroid2.GetComponentInParent<BlueDino1Script>().enabled = false;
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
