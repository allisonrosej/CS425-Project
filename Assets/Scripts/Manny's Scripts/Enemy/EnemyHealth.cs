using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy Health Settings:")]
    public float startingHealth = 10f;
    public float currentHealth;
    public AudioSource enemyTakeDamageSound;

    public AudioSource enemyDeathSound;

    public float iFrameDur = 1;
    public int numOfFlashes = 3;
    private SpriteRenderer spriteRen;
    private Animator anim;
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRen = GetComponent<SpriteRenderer>();
    }

    // TakeDamage() method takes a float and subtracts it from the current Health value. It triggers the hurt 
    // animation if health is greater than zero and the death animation if health is <= 0
   public void TakeDamage(float damage)
    {
        currentHealth -= damage; 

        if (currentHealth > 0)
        {
            // Enemy hurt
            anim.SetTrigger("Hurt");
          
            StartCoroutine(Iframes());  // add iframes 
            enemyTakeDamageSound.Play();
        }

        else
        {
            // Enemy dies
            anim.SetTrigger("Die");
            enemyDeathSound.Play();
        }
    }

    // Iframes method gives the enemy invisible frames from the player by ignoring the collision between the enemy and the 
    // player. The method also flashes the enemy red and white when they are in iframes.
    private IEnumerator Iframes()
    {
        Physics2D.IgnoreLayerCollision(10, 9, true);

        // iframes 
        for (int i = 0; i < numOfFlashes; i++)
        {
            spriteRen.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDur / (numOfFlashes * 2));
            spriteRen.color = Color.white;
            yield return new WaitForSeconds(iFrameDur / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 9, false);


    }

    // Deactivate() method deactivates the enemy gameobject at the end of the death animation of the enemy.
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // RedAnimation() method is called in the first second of the death animation to make the enemy sprite look red.
    private void RedAnimation()
    {
        spriteRen.color = new Color(1, 0, 0, 0.5f);
    }
}
