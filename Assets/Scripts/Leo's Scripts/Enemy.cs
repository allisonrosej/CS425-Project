using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Talks to Player_Movement script
    public Player_Movement playerMovement;
    // Talks to Health script 
    public Health health;

    public Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");

        animator.SetFloat("moveX", moveX);

        bool isMoving = Mathf.Approximately(moveX, 0f);

    }

    // If player collides with enemy, knockback and deal 1 damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

           
            //health = collision.GetComponent<Health>();

            // Resets KBCounter to be equal to KBTotalTime
            playerMovement.KBCounter = playerMovement.KBTotalTime;

            // If player is on the left, hit from right side
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }

            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }

            health.TakeDamage(1);
        }
    }


}
