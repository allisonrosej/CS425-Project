using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float distanceToAttackPlayer = 6;
    private float distance;

    public Animator animator;

    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    public void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (distance < distanceToAttackPlayer)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            animator.SetBool("isMoving", true);
        }



        // Enemy sprite flips depending on player position
        this.spriteRenderer.flipX = player.transform.position.x > this.transform.position.x;

    }
}
