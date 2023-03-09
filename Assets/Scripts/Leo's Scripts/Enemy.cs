using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Talks to Health script 
    public Health playerHealth;

    public int damage;
    private float coolDownTimer = Mathf.Infinity;
    public float attackCoolDown;

    public Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer += Time.deltaTime;


    }

    // If player collides with enemy, knockback and deal 1 damage
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {



            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                animator.SetTrigger("Attack");
                playerHealth.TakeDamage(damage);
            }

            // Resets KBCounter to be equal to KBTotalTime
            //playerMovement.KBCounter = playerMovement.KBTotalTime;

            // If player is on the left, hit from right side
            if (collision.transform.position.x <= transform.position.x)
            {
                //playerMovement.KnockFromRight = true;
            }

            if (collision.transform.position.x > transform.position.x)
            {
                //playerMovement.KnockFromRight = false;
            }

            //animator.SetTrigger("Attack");
            
        }
    }


}
