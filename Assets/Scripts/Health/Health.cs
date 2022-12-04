using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

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

    void TakeDamage(int damage)
    {
        health -=damage; 

        if (health <= 0)
        {
            //Death animation
        }

    }
}
