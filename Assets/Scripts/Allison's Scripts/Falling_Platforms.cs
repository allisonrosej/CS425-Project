using System.Collections;
using UnityEngine;

public class Falling_Platforms : MonoBehaviour
{
    // the delay time for the platform to fall
    private float fallDelay = 1f;
    // the delay time for the platform to be destroyed
    private float destroyDelay = 2f;

    //  refernece fo the Rigidbody2D rb
    [SerializeField] private Rigidbody2D rb;

   // check if the player collided with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // check if the collider has the gameobject has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // will loop the Fall() function
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        // when player collides with the platform, there  is a short delay time
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
       // destroys the platform 
        Destroy(gameObject, destroyDelay);
    }
}
