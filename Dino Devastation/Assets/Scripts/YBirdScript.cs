using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBirdScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform t;
    public float velocity = 5f;
    private float flyTimeLimit = 1.4f;
    private float startTime;
    private bool facingRight = true;
    
    private void goUp() {
        rb.velocity = Vector2.up * velocity;
    }

    private void flipCharacter()
    {
        facingRight = !facingRight;
        t.transform.Rotate(0f, 180f, 0f);
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
            flipCharacter();
            goUp();
            startTime = Time.time;
        }
    }
}
