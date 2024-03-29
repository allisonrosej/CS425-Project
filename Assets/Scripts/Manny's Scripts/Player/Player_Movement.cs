using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Player_Movement : MonoBehaviour
{
    [Header("Movement Settings:")]
    public float currentSpeed = 350f;
    public float horizontalDir;
    public bool canMove = true;
    public float gravity = 6f;

    [Header("Jump Settings:")]
    public float jumpForce = 22f;
    public int MaxAirJumps = 1;
    public int currentJumps = 0;
    public float maxFallingSpeed = 20;
    public float airControl = 1;
    public bool controlJumpHeight = true;
    public bool fasterFalling = true;
    private int jumps;
    private CoyoteTime coyoteTime;

    [Header("Wall Sliding Settings:")]
    public float wallSlideSpeed = 2;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public Transform wallDetection;
    public bool enableWallSlide = true;

    [Header("Wall Jump Settings:")]
    public float wallJumpForce = 25f;
    public float jumpDelay = 0.5f;
    public bool EnableWallJump = true;

    [Header("Player States Settings:")]
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

    [Header("Dashing Settings:")]
    public bool canDash = true;
    public bool movementCanDash = true;
    public bool isDashing;
    public float dashingPower = 3f;
    public float dashingTime = 0.2f;
    public float dashingCooldown = 0.5f;
    public GameObject dashSmokePrefab;
    private GameObject dashSmoke;
    public float dashDur = 0.15f;

    [Header("Demo Settings:")]
    public bool demo;

    [Header("Audio Settings:")]
    public AudioSource jumpSound;
    public AudioSource dashSound;



    // Start is called before the first frame update
    void Start()
    {
        GetComponents();

        rb.gravityScale = gravity;
        //dashSmoke = Instantiate(dashSmokePrefab, transform);
        dashSmokePrefab.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        // for demo player wont move
        if (demo)
            return;

        // pause input for player 
        if (pauseInput)
            return;

        horizontalDir = Input.GetAxisRaw("Horizontal");
        Animation();
        PlayerRotation();
        UpdatePlayerStates();
        BetterJump();
        Jump();
        Dash();
        
    }

    private void FixedUpdate()
    {
        Movement();
        WallSlide();
        CheckGrounded();
        Falling();

    }


    // Animation method triggers the walking and wall sliding animations
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

    // Respawns player and enable movement and input
    public void Respawn()
    {
        animator.SetTrigger("isJumping");
        canMove = true;
        pauseInput = false;
        
        
    }

    // Disables the players movement and set velocity to zero in animation
    void DisableMovement()
    {
        canMove = false;
        rb.velocity = new Vector2(0, 0);
    }

    // Enables the player movement in the animations
    void EnableMovement()
    {
        canMove = true;
    }

    // Disables the input from the player in certain animations .
    void DisableInput()
    {
        pauseInput = true;
    }

    // Enable input of the player in certain animations
    public void EnableInput()
    {
        pauseInput = false;
    }

    // Movement method applies different velocity when on ground or in the air.
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

    // Player rotation method probably rotates the player on direction
    void PlayerRotation()
    {
        if (horizontalDir != 0 && canMove)
        {
            // if facing right no rotation
            if (horizontalDir > 0)
            {

                isFacingRight = true;
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            // if facing left rotate on the y axis 
            else if (horizontalDir < 0)
            {
                isFacingRight = false;
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }

    // UpdatePlayerStates method updates the walking and standing conditions for animations
    void UpdatePlayerStates()
    {
        // if player is not moving
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

    // Better Jump method makes jump less floaty by adding maxjump heights and falling multipliers
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

    // Jump() Method decides to use ground jump when player is grounded and air jump when play in the air 
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("isJumping");
            Debug.Log("Jump");
            if (!isSliding)
            {
                if ((!isInAir || (coyoteTime.CanCoyoteJump())) && jumps < 1 ) 
                    GroundJump();
                else AirJump();
            }
            else
            {
                WallJump();
            }
        }
    }

    // GroundJump() method applies vertical velocity for the jump
    void GroundJump()
    {
        // apply a vertical velocity for the jump
        rb.velocity = Vector2.up * jumpForce;
        jumps++;
        isInAir = true;
        canMove = true;
        jumpSound.Play();
    }

    // Airjump() method check if the player can preform a double jump
    void AirJump()
    {
        if (currentJumps >= MaxAirJumps)
            return;

        rb.gravityScale = gravity;
        rb.velocity = new Vector2(0, 0);
        rb.velocity = Vector2.up * jumpForce;
        currentJumps++; // increment jumps to make sure it not over MaxAirJumps
        jumpSound.Play();
    }

    // Walljump method adds the force to perform a walljump and rotates the player in the right direction
    async void WallJump()
    {
        if (!EnableWallJump)
            return;
        // initially make velocity zero
        rb.velocity = Vector2.zero;

        rb.AddForce(new Vector2(wallJumpForce / 2 * -horizontalDir, wallJumpForce), ForceMode2D.Impulse);
        pauseInput = true;

        horizontalDir = -horizontalDir;

        // facing right
        if (horizontalDir > 0)
        {
            isFacingRight = true;
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        // facing left
        else if (horizontalDir < 0)
        {
            isFacingRight = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        await Task.Delay((int)(jumpDelay * 1000)); // duration after you get control back 

        pauseInput = false;
        horizontalDir = -(horizontalDir);
    }

    // wallslide method checks if player is touch wall and perferms a downward velocity to give the illuion of wall sliding
    void WallSlide()
    {
        // check if the player is touching the wall
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

    // CheckGrounded method check if the player is grounded by using raycasting.
    void CheckGrounded()
    {
        // Make sure you set the ground layer to the ground
        RaycastHit2D ray;
        
        // sets the position of the ray
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
        // if it hits something then its grounded
        if (ray.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    // Falling method sets the falling speed for the player and updates the player states
    void Falling()
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

                if (rb.velocity.y <= -maxFallingSpeed) // sets new fall speed
                    rb.velocity = new Vector3(rb.velocity.x, -maxFallingSpeed, rb.velocity.y);

            }
            else
                isFalling = false;
        }

    }

    // Resets the jump count when colliding in to the wall or ground.
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
                jumps = 0;
                isFalling = false;
            }
        }
    }

    // Dash method calls the Dashing method under certain conditions
    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!canDash)
                return;

            // if player pressed L and is moving in a direction to call the dashing method.
            if (horizontalDir != 0 && canDash)
            {
                animator.SetTrigger("Dash");
                dashSmokePrefab.SetActive(true);    // Triggers smoke animation for dash
                StartCoroutine(Dashing());
                dashSound.Play();
            }
        }

    }

    // Dashing method applies new velocity for dash and waits a few seconds until retrigger can dash
    IEnumerator Dashing()
    {
        canDash = false;
        currentSpeed = currentSpeed * dashingPower;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        yield return new WaitForSeconds(dashDur);
        //dashSmoke.SetActive(false);

        rb.gravityScale = gravity;
        currentSpeed = currentSpeed / dashingPower;
        yield return new WaitForSeconds(dashingCooldown - dashDur);
        canDash = true;
      
    }
}
