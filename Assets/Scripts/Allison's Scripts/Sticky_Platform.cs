using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky_Platform : MonoBehaviour
{
   
    private void OnCollisionEnter2D(Collision2D collision)
    {  
    // checks the collision gamobject name is the player
        if (collision.gameObject.name == "Player")
        {
            // if there is a collision with the collider the player will move along the platform 
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {  // checks the collision gamobject name is the player
        if (collision.gameObject.name == "Player")
        {
           //   if there is no collison with the collider the player can get off of the plaftform
            collision.gameObject.transform.SetParent(null);
        }

    }
 }

