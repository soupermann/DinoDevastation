using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBirdScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform t;
    public float velocity = 1f;
    private float flyTimeLimit = 1f;
    private float startTime;
    public float moveSpeed = 0.01f;
    private bool facingRight = true;


    private void goUp() {
        rb.velocity = Vector2.up * velocity;
    }

    private void flipCharacter()
    {
        facingRight = !facingRight;
        t.transform.Rotate(0f, 180f, 0f);
    }

    private void move() {
        if (t.transform.position.x <= -3) {
            flipCharacter();
        }
        if (t.transform.position.x >= 3) {
            flipCharacter();
        }
        if (facingRight) {
            t.transform.position = new Vector3(t.transform.position.x + moveSpeed, t.transform.position.y,0f);
        }
        if (!facingRight) {
            t.transform.position = new Vector3(t.transform.position.x - moveSpeed, t.transform.position.y,0f);
        }
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = GetComponent<Transform>();
        goUp();
        startTime = Time.time;
    }

   
    void FixedUpdate()
    {
        if (Time.time - startTime >= flyTimeLimit) {
            goUp();
            startTime = Time.time;
        }
        move();
    }
}
