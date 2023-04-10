using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteTime : MonoBehaviour
{
    [SerializeField] private float coyoteTime = 0.2f; // Time in seconds to allow for Coyote Time
    private bool isGrounded = false;
    private float coyoteTimeCounter = 0.0f;

    private void Update()
    {
        // Check if the player is grounded
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has collided with the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the player has left the ground
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    // Method to check if the player can still jump using Coyote Time
    public bool CanCoyoteJump()
    {
        return coyoteTimeCounter > 0;
    }
}
