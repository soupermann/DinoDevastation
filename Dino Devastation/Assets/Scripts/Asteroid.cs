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
	private float distanceTraveled;
	
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
        distanceTraveled = Vector2.Distance(initialPosition, transform.position);
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
				thrown=true;
				gameObject.SetActive(false);
                Respawn();
            }
        }

        // Respawn the object if it has stopped moving for the specified time
        if (!isMoving &&  timeSinceStop  >= respawnTime && thrown == true)
        {
			thrown=true;
			gameObject.SetActive(false);
            Respawn();
			
        }
    }
	IEnumerator RespawnCoroutine(float timeToRespawn)
    {
        
         if (thrown){ 
		
		
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
		//Destroy(GameObject.FindWithTag("Asteroid"));
		gameObject.SetActive(false);
		mainCamera.enabled = true;
		MonoBehaviour camMono = Camera.main.GetComponent<MonoBehaviour>();
//Use it to start your coroutine function
        camMono.StartCoroutine(RespawnCoroutine(2f));
		
        
        
		
        
		
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
		// Do this for each dino
		if(collision.gameObject.CompareTag("BDR"))
        {
			float baseDamage = 20f; // Set the base damage of the object
			float maxDistance = 10f;
			float damage = baseDamage * Mathf.Clamp(distanceTraveled / maxDistance, 0f, 1f);
			var BDR = GameObject.FindWithTag("BDR");
			counter = counter + damage;
			Debug.Log("DINO HITTTTT"); // Testing to see if collision tag works.
			healthManager.TakeDamage(damage);
			if(healthManager.healthAmount+healthManager0.healthAmount <= 0)
			{
				instance = "Red";
				PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
				SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
			}
			if(healthManager.healthAmount <= 0)
			{
				//Destroy(BDR);
				BDR.SetActive(false);
			}
        }
		if (collision.gameObject.CompareTag("BDL"))
		{
			float baseDamage = 20f; // Set the base damage of the object
			float maxDistance = 10f;
			float damage = baseDamage * Mathf.Clamp(distanceTraveled / maxDistance, 0f, 1f);
			var BDL = GameObject.FindWithTag("BDL");
			counter = counter + damage;
			Debug.Log("DINO HITTTTT 000000"); // Testing to see if collision tag works.
			healthManager0.TakeDamage(damage);
			if(healthManager0.healthAmount+healthManager.healthAmount <= 0)
			{
				instance = "Red";
				PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
				SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
			}
			if(healthManager0.healthAmount <= 0)
			{
				//Destroy(BDL);
				BDL.SetActive(false);
			}
		}
		if (collision.gameObject.CompareTag("RDR"))
		{
			float baseDamage = 20f; // Set the base damage of the object
			float maxDistance = 10f;
			float damage = baseDamage * Mathf.Clamp(distanceTraveled / maxDistance, 0f, 1f);
			var RDR = GameObject.FindWithTag("RDR");
			counter2 = counter2 + damage;
			Debug.Log("DINO HITTTTT 11111"); // Testing to see if collision tag works.
			healthManager1.TakeDamage(damage);
			if(healthManager1.healthAmount+healthManager2.healthAmount <= 0)
			{
				instance = "Blue";
				PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
				SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
			}
			if(healthManager1.healthAmount <= 0)
			{
				//Destroy(RDR);
				RDR.SetActive(false);
			}
		}
		if (collision.gameObject.CompareTag("RDL"))
		{
			float baseDamage = 20f; // Set the base damage of the object
			float maxDistance = 10f;
			float damage = baseDamage * Mathf.Clamp(distanceTraveled / maxDistance, 0f, 1f);
			var RDL = GameObject.FindWithTag("RDL");
			counter2 = counter2 + damage;
			Debug.Log("DINO HITTTTT 222222"); // Testing to see if collision tag works.

			healthManager2.TakeDamage(damage);
			if (healthManager1.healthAmount+healthManager2.healthAmount <= 0)
			{
				instance = "Blue";
				PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
				SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
			}
			if(healthManager2.healthAmount <= 0)
			{
				//Destroy(RDL);
				RDL.SetActive(false);
			}
		}
	}
}
