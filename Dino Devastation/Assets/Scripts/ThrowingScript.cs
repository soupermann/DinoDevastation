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

	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;

	//public GameObject objectToDisable; BALL
	public bool isEnabled = false;

	public void OnButtonClick()
	{
		isEnabled = !isEnabled; // toggle the value, only let is be available when clicked.
	}


	//---------------------------------------
	public void ThrowAsteroid()
    {
		// Used for onclick call to throw asteroid.
		Start();
		Update();
		OnDragStart();
		OnDrag();
		OnDragEnd();
    }
	void Start()
	{
		cam = Camera.main;
		ball.DesactivateRb();
	}

	void Update()
	{
		ball.gameObject.SetActive(isEnabled);

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
	}

}
