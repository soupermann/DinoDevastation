using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	public float damage = 10f;
	public static float counter;
	public static float counter2;
	public static string instance;

	public HealthManager healthManager;
	public HealthManager healthManager0;
	public HealthManager healthManager1;
	public HealthManager healthManager2;
	

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

    public void OnCollisionEnter2D(Collision2D collision)
    {
		// Do this for each dino
		if(collision.gameObject.CompareTag("BDR"))
        {
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
            Destroy(GameObject.FindWithTag("BDR"));
			}
        }
		if (collision.gameObject.CompareTag("BDL"))
		{
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
				Destroy(GameObject.FindWithTag("BDL"));
			}
		}
		if (collision.gameObject.CompareTag("RDR"))
		{
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
				Destroy(GameObject.FindWithTag("RDR"));
			}
		}
		if (collision.gameObject.CompareTag("RDL"))
		{
			counter2 = counter2 + damage;
			Debug.Log("DINO HITTTTT 222222"); // Testing to see if collision tag works.
			healthManager2.TakeDamage(damage);
			if(healthManager1.healthAmount+healthManager2.healthAmount <= 0)
			{
				instance = "Blue";
				PlayerPrefs.SetInt("lastLevel", SceneManager.GetActiveScene().buildIndex);
				SceneManager.LoadScene("Scoresheet", LoadSceneMode.Single);
			}
			if(healthManager2.healthAmount <= 0)
			{
				Destroy(GameObject.FindWithTag("RDL"));
			}
		}
	}
}
