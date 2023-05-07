using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning_platforms : MonoBehaviour
{
   // variables
    public Rigidbody2D rbd;
    
    private BoxCollider2D boxcollider;

    private float fallplat;

    private float fallplaton;

    private bool isFalling = false;

    private Vector2 initialposition;

// initializes the varaibles rbd and box collider before the game starts
    private void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {  // sets the  intial postion to the transform position
        initialposition = transform.position;
    }

 // checks  if there player collison 
    private void OnCollisionEnter2D(Collision2D col)
    {
       // checks if the col.tag is the player tag
        if (col.gameObject.CompareTag("Player"))
        
        {
            //  wwill call "Fall" and fallplat 
            Invoke("Fall", fallplat);
        }
    } 

    //  Fall function: makes the platform fall
    void Fall()
    {
        // sets rbd
        rbd.isKinematic = false;
        // if the player collides with the box collider it will equal to true
        boxcollider.isTrigger = true;
        // the platform will fall
        isFalling = true;

    }
   
    // respaen function: brings back the platform after if falls
    void respawn()
    {
        // loops the respawnco function
        StartCoroutine(respawnco());
    }

  // 
    IEnumerator respawnco()
    {
        // waits for a few seconds before the platform respawns
        yield return new WaitForSeconds(fallplaton);
        // the platform is not falling 
        isFalling = false;
       // rbbd is kinematric       
        rbd.isKinematic = true;
        // the box collider is not trigger by the player
        boxcollider.isTrigger = false;
       // the platform returns to its intial postion
        transform.position = initialposition;
        // set the rigid body velocity to Vector2.zero
        rbd.velocity = Vector2.zero;
    }
// check if there is player triggers a collison
    private void OnTriggerEnter2D(Collider2D col)
    {
    // checks if the col.tag is the player tag
        if (col.tag == "Player")

        {
            // // rbbd is kinematric   
            rbd.isKinematic = true;
            //  the player is not triggering the box collider
            boxcollider.isTrigger = false;
            // the platform is not falling
            isFalling = false;
            // the respawn function is called
            respawn();
        }
    }
}

