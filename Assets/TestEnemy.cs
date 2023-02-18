using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth; 

    void Start()
    {
        currentHealth = maxHealth; 

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 

        if (currentHealth <= 0)
        {
            EnemyDeath(); 
        }
    }

    void EnemyDeath()
    {
        Debug.Log("Enemy Died");
    }
}
