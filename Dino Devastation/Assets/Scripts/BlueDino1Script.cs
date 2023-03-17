using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueDino1Script : MonoBehaviour
{
    public float moveSpeed = 5;
    public float jumpForce = 250;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius = 1;
    public int maxJumpCount = 1;


    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;


    // Awak is called agter all objects are initialized
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Will look for a compenent on this GameObject (what the script is attached to) of type RigiBody2D
    }

    public void Start()
    {
        jumpCount = maxJumpCount;

    }
    // Update is called once per frame
    void Update()
    {
        // Get inputs
        GetInputs();

        //Anmiate
        Animate();

    }

    // Can be handeled multiple times per frame.
    private void FixedUpdate()
    {
        // Check to see if we are grounded to make sure no double jumps are allowed
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);

        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        // Move
        Move();
    }
    private void Move()
    {
        // Move
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            //flipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            //flipCharacter();
        }
    }
    private void GetInputs()
    {
        // Get inputs
        moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 to 1
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount > 0)
        {
            isJumping = true;
        }
    }


    private void flipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
