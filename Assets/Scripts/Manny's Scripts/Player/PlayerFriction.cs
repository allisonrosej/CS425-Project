using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFriction : MonoBehaviour
{
    [Header("Player Friction Settings: ")]
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

    // ModifyFriction() method modifies the players friction depending if the player is dead or alive.
    void ModifyFriction()
    {
        if (playerHealth.playerdead == true)
        {
            rb.velocity = Vector2.zero;     // player velocity to zero
            rb.sharedMaterial.friction = frictionDead; // set friction to 1
            playerMovement.horizontalDir = 0f;
        }
        else
        {
            rb.sharedMaterial.friction = frictionAlive; // set friction to 0
        }
    }
}
