using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private Animator anim;
    public bool playerdead;

    // Leo's code for iframes. This variable keeps track if player is invincible
    private bool isInvincible = false;

    [SerializeField]
    private float invincibilityDurationSeconds;


    // Leo's code for iframes
    public IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDurationSeconds);
        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerdead = false;
    }

    private void Update()
    {
        DisplayHealth();
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
        }
    }

    void DisplayHealth()
    {
        // Set health equal to number of hearts
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            //if i less than health than make sprite full heart
            if (health >= 5)
            {
                health = 5;
            }
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {   // if i greater than health then set those sprites to empty heart
                hearts[i].sprite = emptyHeart;
            }
        }

        

    }

    public void TakeDamage(int damage)
    {

        // Iframes
        if (isInvincible) return;

        health -= damage;

        if (health <= 0)
        {
            playerdead = true;
            //Death animation
            anim.SetTrigger("Die");
            LevelManager.instance.GameOver();
            
            //gameObject.SetActive(false);
            
        }
        if (health > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(BecomeTemporarilyInvincible());
        }
            



    }

}
