using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    // Make dinos die when collided
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BDL") || collision.gameObject.CompareTag("BDR") || collision.gameObject.CompareTag("RDL") || collision.gameObject.CompareTag("RDR"))
        {
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
            // TODO : CALL DIE WHEN WE HAVE DYING ANNIMATION
        }
    }

    void Die()
    {
        // TODO : Probably best to set players health to 0 and then have trigger when health is 0 he dies
    }

}
