using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 10f;
    public float currentHealth;

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

   public void TakeDamage(float damage)
    {
        currentHealth -= damage; 

        if (currentHealth > 0)
        {
            // Enemy hurt
            anim.SetTrigger("Hurt");
            StartCoroutine(Iframes());
            // add iframes 


        }

        else
        {
            // Enemy dies
            anim.SetTrigger("Die");
            
        }
    }

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

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
