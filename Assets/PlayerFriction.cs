using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriction : MonoBehaviour
{
    private Health playerHealth;
    private Rigidbody2D rb;
    private Player_Movement playerMovement;

    private float frictionAlive = 0.0f;
    private float frictionDead = 1f;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<Player_Movement>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ModifyFriction();
    }
    void ModifyFriction()
    {
        if (playerHealth.playerdead == true)
        {
            rb.velocity = Vector2.zero;
            rb.sharedMaterial.friction = frictionDead;
            playerMovement.horizontalDir = 0f;
        }
        else
        {
            rb.sharedMaterial.friction = frictionAlive;
        }
    }
}
