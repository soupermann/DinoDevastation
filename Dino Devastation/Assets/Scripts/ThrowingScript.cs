using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingScript : MonoBehaviour
{
	#region Singleton class: GameManager

	public static ThrowingScript Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	#endregion

	Camera cam;

	public Asteroid ball;
	public Trajectory trajectory;
	[SerializeField] float pushForce = 4f;

	bool isDragging = false;

	public bool hasBeenThrown = false; // Make sure it only gets thrown once


	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;



	//---------------------------------------
	//public void ThrowAsteroid()
 //   {
	//	// Used for onclick call to throw asteroid.
	//	Start();
	//	Update();
	//	OnDragStart();
	//	OnDrag();
	//	OnDragEnd();
 //   }
	void Start()
	{
		cam = Camera.main;
		ball.DesactivateRb();
	}

	void Update()
	{
		if (!hasBeenThrown) // only allow dragging if the asteroid hasn't been thrown yet
		{
			if (Input.GetMouseButtonDown(0))
			{
				isDragging = true;
				OnDragStart();
			}
			if (Input.GetMouseButtonUp(0))
			{
				isDragging = false;
				OnDragEnd();
			}

			if (isDragging)
			{
				OnDrag();
			}
		}
	}

	//-Drag--------------------------------------
	void OnDragStart()
	{
		ball.DesactivateRb();
		startPoint = cam.ScreenToWorldPoint(Input.mousePosition);

		trajectory.Show();
	}

	void OnDrag()
	{
		endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		distance = Vector2.Distance(startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		Debug.DrawLine(startPoint, endPoint);


		trajectory.UpdateDots(ball.pos, force);
	}

	void OnDragEnd()
	{
		//push the ball
		ball.ActivateRb();

		ball.Push(force);

		trajectory.Hide();

		hasBeenThrown = true;

		ball.GetComponent<Rigidbody2D>().gravityScale = 1f;
	}

}
