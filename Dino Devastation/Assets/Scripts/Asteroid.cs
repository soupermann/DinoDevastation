using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public CircleCollider2D col;


    [HideInInspector] public Vector3 pos { get { return transform.position; } }
    public Camera mainCamera;
    public Camera secondCamera;

    public static float counter;
    public static float counter2;
    public static string instance;
    public bool respawnOnlyWhenThrown = true;
    public HealthManager healthManager;
    public HealthManager healthManager0;
    public HealthManager healthManager1;
    public HealthManager healthManager2;
    private Vector2 initialPosition;

    private float damage = 15f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    public void Push(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void ActivateRb()
    {
        rb.isKinematic = false;
    }

    public void DesactivateRb()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
    }
    public float respawnTime = 2f;
    public float respawnOffset = 0.5f;
    public float respawnThreshold = 0.01f;
    private bool thrown = false;


    private Vector3 startPos;
    private Quaternion startRot;
    private float timeSinceStop;
    private bool isMoving;

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        startRot = transform.rotation;
        timeSinceStop = 0f;
        isMoving = false;
    }
    public void ThrowObject(Vector3 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
        thrown = true; // Mark the object as thrown
    }

    void Update()
    {
        if (rb.velocity.magnitude > respawnThreshold)
        {
            timeSinceStop = 0f;
            isMoving = true;

        }
        else
        {
            timeSinceStop += Time.deltaTime;
            isMoving = false;
        }

        // Check if the object is offscreen
        if (mainCamera != null)
        {
            Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
            if (screenPos.x < 0f || screenPos.x > 1f || screenPos.y < 0f || screenPos.y > 1f)
            {
                thrown = true;
                gameObject.SetActive(false);
                Respawn();
            }
        }

        // Respawn the object if it has stopped moving for the specified time
        if (!isMoving && timeSinceStop >= respawnTime && thrown == true)
        {
            thrown = true;
            gameObject.SetActive(false);
            Respawn();

        }
    }
    IEnumerator RespawnCoroutine(float timeToRespawn)
    {

        if (thrown)
        {
            yield return new WaitForSeconds(timeToRespawn);
            gameObject.SetActive(true);
            transform.position = startPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0f;
            thrown = false;
            secondCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }

    public void Respawn()
    {
        // Respawning asteroid
        gameObject.SetActive(false);
        mainCamera.enabled = true;
        MonoBehaviour camMono = Camera.main.GetComponent<MonoBehaviour>();
        //Use it to start your coroutine function
        camMono.StartCoroutine(RespawnCoroutine(2f));

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Call IfHit when each dino is hit with asteroid
        if (collision.gameObject.CompareTag("BDR"))
        {
            IfHit(GameObject.FindWithTag("BDR"), healthManager, healthManager0, "Red");
        }
        if (collision.gameObject.CompareTag("BDL"))
        {
            IfHit(GameObject.FindWithTag("BDL"), healthManager0, healthManager, "Red");

        }
        if (collision.gameObject.CompareTag("RDR"))
        {
            IfHit(GameObject.FindWithTag("RDR"), healthManager1, healthManager2, "Blue");

        }
        if (collision.gameObject.CompareTag("RDL"))
        {
            IfHit(GameObject.FindWithTag("RDL"), healthManager2, healthManager1, "Blue");
        }
    }

    // Function to handle when dino is hit with asteroid
    public void IfHit(GameObject dino, HealthManager healthManagerOne, HealthManager healthManagerTwo, string color)
    {
        counter = counter + damage;
        healthManagerOne.TakeDamage(damage);
        if (healthManagerOne.healthAmount + healthManagerTwo.healthAmount <= 0)
        {
            instance = color;
            PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
        }
        if (healthManagerOne.healthAmount <= 0)
        {
            dino.SetActive(false);
        }

    }

}
