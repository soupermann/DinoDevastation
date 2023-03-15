using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[HideInInspector] public Rigidbody2D rb;
	[HideInInspector] public CircleCollider2D col;

	[HideInInspector] public Vector3 pos { get { return transform.position; } }

	public float damage = 10f;

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
		if(collision.gameObject.CompareTag("Player"))
        {
			Debug.Log("DINO HITTTTT"); // Testing to see if collision tag works.
			healthManager.TakeDamage(damage);
        }
		if (collision.gameObject.CompareTag("BDL"))
		{
			Debug.Log("DINO HITTTTT 000000"); // Testing to see if collision tag works.
			healthManager0.TakeDamage(damage);
		}
		if (collision.gameObject.CompareTag("RDR"))
		{
			Debug.Log("DINO HITTTTT 11111"); // Testing to see if collision tag works.
			healthManager1.TakeDamage(damage);
		}
		if (collision.gameObject.CompareTag("RDL"))
		{
			Debug.Log("DINO HITTTTT 222222"); // Testing to see if collision tag works.
			healthManager2.TakeDamage(damage);
		}
	}
}
