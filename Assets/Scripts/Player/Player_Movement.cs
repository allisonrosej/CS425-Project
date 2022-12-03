using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed;
    public float airMoveSpeed = 12f;
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


    [Header("Wall Sliding Settings")]
    public float wallSlidingSpeed = 0;
    public LayerMask wallLayer;
    public Transform wallCheck;
    public Vector2 WallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("Wall Jump Settings")]
    public float wallJumpForce = 18f;
    public float wallJumpDir = -1f;
    public Vector2 wallJumpAngle;

 [Header("Dashing Settings")]
    public bool canDash = true;
    public bool movementCanDash = true;
    public bool isDashing;
    public float dashingPower = 125f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
     [SerializeField] public TrailRenderer tr; 
   
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallJumpAngle.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        Movement();
        CheckArea();
        Jump();
        WallSlide();
        WallJump();
        
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            movementCanDash = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))  
        {
            movementCanDash = true;
        }
        
        Dash();  
        if (Input.GetKeyDown(KeyCode.X) && canDash && movementCanDash)
        {
            StartCoroutine(Dash()); // This need to change
        }
        
    }

    void CheckArea()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, wallLayer);
    }

    void Movement()
    {
        // For animations
        if (horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isGrounded)
        {
            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        }
        else if(!isGrounded && !isWallSliding && horizontalInput != 0)
        {
            rb.AddForce(new Vector2(airMoveSpeed * horizontalInput, 0));
            if (Mathf.Abs(rb.velocity.x) > speed )
            {
                rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            }
        }
        //else if(isTouchingWall && horizontalInput != 0)
        //{
        //    rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        //}


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
           


    }

    void WallSlide()
    {
        if (isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;

        }
        else
        {
            isWallSliding = false;

        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlidingSpeed);
        }
    }

    void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDir * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
        }
    }
    void Flip()
    {
        if (!isWallSliding)
        {
            wallJumpDir *= -1;
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x = temp.x * -1;
            transform.localScale = temp;
        }
    }
    IEnumerator Dash() 
    {
       

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale; // allows player not to be affected by gravity
        rb.gravityScale = 0f; // sets gravity to zero
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true; //display trail (IDK if we want this)
        yield return new WaitForSeconds(dashingTime); // stops the player from dashing forever
        tr.emitting = false; // stops displaying the trail render ( animation tail following the character)
        rb.gravityScale = originalGravity;  // sets the gravity to og scale
        isDashing = false; // stops dashing
        yield return new WaitForSeconds(dashingCooldown); // allows a dash cooldown
        canDash = true; // player can dash again after cooldown is done

         
    }
   
}
