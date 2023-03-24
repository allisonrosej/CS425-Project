using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float currentSpeed = 350f;
    public float horizontalDir;
    public bool canMove = true;
    public float gravity = 6f;

    [Header("Jump Settings")]
    public float jumpForce = 22f;
    public int MaxAirJumps = 1;
    public int currentJumps = 0;
    public float maxFallingSpeed = 20;
    public float airControl = 1;
    public bool controlJumpHeight = true;
    public bool fasterFalling = true;

    [Header("Wall Sliding Settings")]
    public float wallSlideSpeed = 2;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public Transform wallDetection;
    public bool enableWallSlide = true;

    [Header("Wall Jump Settings")]
    public float wallJumpForce = 25f;
    public float jumpDelay = 0.5f;
    public bool EnableWallJump = true;

    [Header("Player States Settings")]
    public bool isFacingRight;
    public bool isStanding;
    public bool isWalking;
    public bool isInAir;
    public bool isFalling;
    public bool isSliding;
    public bool isGrounded;
    public bool touchingWall;

    // How powerful knock back will be
    public float KBForce;
    // Counts how long KB lasts
    public float KBCounter;
    // How long KB lasts
    public float KBTotalTime;
    // Keeps track which direction player has been hit from
    public bool KnockFromRight;

    // private members
    private string groundTag = "Ground";
    private string wallTag = "Ground";
    public bool pauseInput = false;
    private Rigidbody2D rb;
    private CapsuleCollider2D capCol;
    private Animator animator;

    [Header("Dashing Settings")]
    public bool canDash = true;
    public bool movementCanDash = true;
    public bool isDashing;
    public float dashingPower = 3f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 0.5f;
     [SerializeField] public TrailRenderer tr;
    public float dashDur = 0.15f;

    public PauseMenu pauseMenuCheck;
    private CoyoteTime coyoteTime;

    // Start is called before the first frame update
    void Start()
    {
        GetComponents();

        rb.gravityScale = gravity;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseInput)
            return;

        horizontalDir = Input.GetAxisRaw("Horizontal");
        Animation();
        RotateToDirection();
        UpdateMovementStates();
        BetterJump();
        Jump();
        Dash();

        //if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        //{
        //    movementCanDash = false;
        //}

        //if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        //{
        //    movementCanDash = true;
        //}
        
        //Dash();  
        //if (Input.GetKeyDown(KeyCode.X) && canDash && movementCanDash)
        //{
        //    StartCoroutine(Dash()); // This need to change
        //}
        
    }

    private void FixedUpdate()
    {
        Movement();
        WallSlide();
        CheckGrounded();
        CheckFalling();

    }

    void Animation()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isWallSliding", isSliding);
    }
    void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        capCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        coyoteTime = GetComponent<CoyoteTime>();

    }
    void DisableMovement()
    {
        canMove = false;
        rb.velocity = new Vector2(0, 0);
    }

    void EnableMovement()
    {
        canMove = true;
    }

    void Movement()
    {
        if (!canMove)
            return;

        if(KBCounter <= 0) // Disable movement when hit
        {
            if (!isInAir)
                rb.velocity = new Vector2(horizontalDir * currentSpeed * Time.fixedDeltaTime, rb.velocity.y);
            else
                rb.velocity = new Vector2(horizontalDir * (airControl * currentSpeed) * Time.fixedDeltaTime, rb.velocity.y);
        } else // If knockback is in effect
        {
            if (KnockFromRight == true)
            {
                rb.velocity = new Vector2(-KBForce, KBForce);
            }
            // Sends player to the right
            if (KnockFromRight == false)
            {
                rb.velocity = new Vector2(KBForce, KBForce);
            }

            // Countdown to prevent knockback lasting forever
            KBCounter -= Time.deltaTime;
        }

       
    }

    void RotateToDirection()
    {
        if (horizontalDir != 0 && canMove)
        {
            if (horizontalDir > 0)
            {

                isFacingRight = true;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (horizontalDir < 0)
            {
                isFacingRight = false;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }

    void UpdateMovementStates()
    {
        if (horizontalDir == 0)
        {
            isStanding = true;
            isWalking = false;

        }
        else
        {
            isStanding = false;
            isWalking = true;

        }
    }

    void BetterJump()
    {
        if (isInAir && !isFalling && !Input.GetKey(KeyCode.Space)) // Stop Jumping when release jump button
        {
            if (controlJumpHeight)
                rb.velocity += Vector2.up * -140 * Time.deltaTime;
        }

        if (fasterFalling && isFalling)
            rb.velocity += Vector2.up * -70 * Time.deltaTime;
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
            Debug.Log("Jump");
            if (!isSliding)
            {
                if (isGrounded || (coyoteTime.CanCoyoteJump())) 
                    GroundJump();
                else AirJump();
            }
            else
            {
                WallJump();
            }
        }
    }

    void GroundJump()
    {
        rb.velocity = Vector2.up * jumpForce;
        isInAir = true;
        canMove = true;
    }

    void AirJump()
    {
        if (currentJumps >= MaxAirJumps)
            return;

        rb.gravityScale = gravity;
        rb.velocity = new Vector2(0, 0);
        rb.velocity = Vector2.up * jumpForce;
        currentJumps++; // increment jumps to make sure it not over MaxAirJumps
    }

    async void WallJump()
    {
        if (!EnableWallJump)
            return;
        // initially make velocity zero
        rb.velocity = Vector2.zero;

        rb.AddForce(new Vector2(wallJumpForce / 2 * -horizontalDir, wallJumpForce), ForceMode2D.Impulse);
        pauseInput = true;

        horizontalDir = -horizontalDir;

        if (horizontalDir > 0)
        {
            isFacingRight = true;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (horizontalDir < 0)
        {
            isFacingRight = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        await Task.Delay((int)(jumpDelay * 1000)); // duration after you get control back 

        pauseInput = false;
        horizontalDir = -(horizontalDir);
    }

    void WallSlide()
    {

        touchingWall = Physics2D.OverlapCircle(wallDetection.position, 0.2f, wallLayer);

        if (touchingWall && !isGrounded && horizontalDir != 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (!enableWallSlide)
            return;

        if (isSliding)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));

    }

    void CheckGrounded()
    {
        // Make sure you set the ground layer to the ground
        RaycastHit2D ray;

        if (transform.rotation.y == 0)
        {
            Vector2 position = new Vector2(capCol.bounds.center.x - capCol.bounds.extents.x, capCol.bounds.min.y);
            ray = Physics2D.Raycast(position, Vector2.down, capCol.bounds.extents.y + 0.02f, groundLayer);
        }
        else
        {
            Vector2 position = new Vector2(capCol.bounds.center.x + capCol.bounds.extents.x, capCol.bounds.min.y);
            ray = Physics2D.Raycast(position, Vector2.down, capCol.bounds.extents.y + 0.02f, groundLayer);
        }

        if (ray.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    void CheckFalling()
    {

        if (isGrounded)
        {
            isInAir = false;
            isFalling = false;
        }
        else
        {
            isInAir = true;

            if (rb.velocity.y < 0)
            {
                isFalling = true;

                if (rb.velocity.y <= -maxFallingSpeed)
                    rb.velocity = new Vector3(rb.velocity.x, -maxFallingSpeed, rb.velocity.y);

            }
            else
                isFalling = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset Jump Counts When Collide With The Ground tag or wall tag
        if (collision.collider.CompareTag(wallTag) || collision.collider.CompareTag(groundTag))
        {
            RaycastHit2D ray;
            ray = Physics2D.Raycast(capCol.bounds.center, Vector2.down, capCol.bounds.extents.y + 0.02f, groundLayer);
            Debug.Log(ray.collider);
            Debug.DrawRay(capCol.bounds.center, Vector2.down, Color.red, 10, false);
            // if the ray.collider is not equal to null it will reset current jumps
            if (ray.collider != null)
            {
                isInAir = false;
                currentJumps = 0;
                isFalling = false;
            }
        }
    }

    //IEnumerator Dash() 
    //{


    //    canDash = false;
    //    isDashing = true;
    //    float originalGravity = rb.gravityScale; // allows player not to be affected by gravity
    //    rb.gravityScale = 0f; // sets gravity to zero
    //    rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
    //    tr.emitting = true; //display trail (IDK if we want this)
    //    yield return new WaitForSeconds(dashingTime); // stops the player from dashing forever
    //    tr.emitting = false; // stops displaying the trail render ( animation tail following the character)
    //    rb.gravityScale = originalGravity;  // sets the gravity to og scale
    //    isDashing = false; // stops dashing
    //    yield return new WaitForSeconds(dashingCooldown); // allows a dash cooldown
    //    canDash = true; // player can dash again after cooldown is done


    //}

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!canDash)
                return;

            if (horizontalDir != 0 && canDash)
            {
                animator.SetTrigger("Dash");
                StartCoroutine(Dashing());
            }
        }

    }

    IEnumerator Dashing()
    {
        canDash = false;
        currentSpeed = currentSpeed * dashingPower;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        yield return new WaitForSeconds(dashDur);


        rb.gravityScale = gravity;
        currentSpeed = currentSpeed / dashingPower;
        yield return new WaitForSeconds(dashingCooldown - dashDur);
        canDash = true;

    }
}
