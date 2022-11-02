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

    public int extraJumps; 



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        
    }
    
    // Use for any physics related aspects
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
        horizontalInput = Input.GetAxis("Horizontal");

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

    void Flip()
    {
        right = !right;
        Vector3 temp = transform.localScale;
        temp.x = temp.x * -1;
        transform.localScale = temp; 


    }
}
