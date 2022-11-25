using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    private bool facingRight = true;
    private float horizontalInput;

    [Header("Jump Settings")]
    public Transform groundCheck;
    public LayerMask Ground;
    public float jumpforce;
    public int extraJumps;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float checkRadius = 0.5f;
    private bool isGrounded;
    
    [Header("Dashing Settings")]
    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    private TrailRenderer tr;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Movement();
        CheckArea();
        Jump();
        DashMove();
    }

    void CheckArea()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
    }

    void Movement()
    {

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);


        if (facingRight == false && horizontalInput > 0)
        {
            Flip();
        }

        else if (facingRight == true && horizontalInput < 0)
        {
            Flip();
        }

    }

    void Jump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        if (isGrounded == true)
        {
            extraJumps = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    
        if (Input.GetKeyDown(KeyCode.D) && canDash)
        {
            StartCoroutine(DashMove());
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 temp = transform.localScale;
        temp.x = temp.x * -1;
        transform.localScale = temp;

    }
     IEnumerator DashMove()
    {
       canDash = false;
        //isDashing = true;
        float originalGravity = rb.gravityScale; // allows player not to be affected by gravity
        rb.gravityScale = 0f; // sets gravity to zero
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true; //display trail (IDK if we want this)
        yield return new WaitForSeconds(dashingTime); // stops the player from dashing forever
        tr.emitting = false; // stops displaying the trail render
        rb.gravityScale = originalGravity;  // sets the gravity to og scale
        //isDashing = false; // stops dashing
        yield return new WaitForSeconds(dashingCooldown); // allows a dashcool down
        canDash = true; // player can dash again after cooldown is done
    }
}






    
   