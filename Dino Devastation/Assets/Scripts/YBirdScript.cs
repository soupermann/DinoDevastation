using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBirdScript : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float velocity = 5f;
    private float flyTimeLimit = 1.4f;
    private float startTime;
    
    private void goUp() {
        rb.velocity = Vector2.up * velocity;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        goUp();
        startTime = Time.time;
    }

    
    void FixedUpdate()
    {
        if (Time.time - startTime >= flyTimeLimit) {
            goUp();
            startTime = Time.time;
        }
    }
}
