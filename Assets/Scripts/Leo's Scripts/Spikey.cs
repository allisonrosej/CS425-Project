using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{

    Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject whatHit = col.gameObject;
        if (whatHit.CompareTag("Player"))
        {
            playerHealth.TakeDamage(5);
        }
    }

}
