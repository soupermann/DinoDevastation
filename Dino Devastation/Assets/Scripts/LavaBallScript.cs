using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallScript : MonoBehaviour
{
    public float amplitude = 1.2f;  // the distance the ball will travel up and down
    public float frequency = 2.0f;  // the speed at which the ball will move

    private float startY;  // the initial y position of the ball

    void Start()
    {
        startY = transform.position.y;  // store the initial y position
    }

    void Update()
    {
        // calculate the new y position based on time
        float newY = startY + amplitude * Mathf.Sin(Time.time * frequency);

        // update the ball's position
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BDL") || collision.gameObject.CompareTag("BDR") || collision.gameObject.CompareTag("RDL") || collision.gameObject.CompareTag("RDR"))
        {
            Destroy(collision.gameObject);
            // TODO : CALL DIE WHEN WE HAVE DYING ANNIMATION
        }
    }

    void Die()
    {
        // TODO : Probably best to set players health to 0 and then have trigger when health is 0 he dies
    }


}