using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed;
    public float jumpforce;
    private float horizontalInput;

    private bool right = true; 
    private Rigidbody2D rb;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask Ground;
<<<<<<< Updated upstream
=======
    public float jumpforce;
    public int extraJumps;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float checkRadius = 0.5f;
    private bool isGrounded;

    [Header("Wall Sliding Settings")]
    public float wallSlideSpeed;
    public LayerMask Wall;
    public Transform wallCheck;
    public Vector2 WallCheckSize;
    private bool isOnWall;
    private bool isWallSliding;

    [Header("Wall Jump Settings")]
    public float wallJumpForce = 5f;
    public float wallJumpDir = -1f;
    public Vector2 wallJumpAngle; 

>>>>>>> Stashed changes

    public int extraJumps; 



    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        rb = GetComponent<Rigidbody2D>();  
        
=======
        horizontalInput = Input.GetAxis("Horizontal");
        Movement();
        CheckArea();
        Jump();
        WallSlide();

>>>>>>> Stashed changes
    }
    
    // Use for any physics related aspects
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
<<<<<<< Updated upstream
        horizontalInput = Input.GetAxis("Horizontal");
=======
        isOnWall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, Wall);
    }

    void Movement()
    {
>>>>>>> Stashed changes

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        // moving left
        if (right == false && horizontalInput > 0)
        {
            Flip(); 
        }
        // moving right 
        else if(right == true && horizontalInput < 0)
        {
            Flip();
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = 1; 
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--; 
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpforce;
        }
    }

<<<<<<< Updated upstream
    void Flip()
    {
        right = !right;
=======
    void WallSlide()
    {
        if (isOnWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true; 

        }
        else
        {
            isWallSliding = false;

        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed);
        }
    }

    void WallJump()
    {
        //if (isWallSliding || isOnWall)
        //{
        //    if ()
        //}
    }
    void Flip()
    {
        wallJumpDir *= -1;
        facingRight = !facingRight;
>>>>>>> Stashed changes
        Vector3 temp = transform.localScale;
        temp.x = temp.x * -1;
        transform.localScale = temp; 


    }
}
