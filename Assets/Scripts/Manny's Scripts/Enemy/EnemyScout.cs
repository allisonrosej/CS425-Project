using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScout : MonoBehaviour
{
    [Header("Enemy Scout Settings:")]
    public Transform leftEdge;
    public Transform rightEdge;

    public Transform enemy;

    public float speed;
    private Vector3 initScale;
    public bool movingLeft;
    private Animator anim;

    public float idleDur;
    public float idleTimer; 
    private void Start()
    {
        initScale = enemy.localScale;
        anim = GetComponentInChildren<Animator>();
        enemy.transform.rotation = new Quaternion(0, 180, 0, 0);

    }
    private void Update()
    {
        if (movingLeft)
        {
            // keep moving in that direction until you reach the edge
            if (enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);

            }

            else
            {
                // change direction
                ChangeDirection();
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                // change direction
                ChangeDirection();
            }
        }
        
    }

    // ChangeDirection() method deactivates the walking animation and changes the direction of the enemy. 
    private void ChangeDirection()
    {
        anim.SetBool("isWalking", false);

        idleTimer += Time.deltaTime;
        
        if (idleTimer > idleDur)
            movingLeft = !movingLeft;
    }

    // MoveInDirection() method moves the enemy in a given direction and also triggers the walking animation the enemy
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("isWalking", true);
        // make enemy face direction 
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        // move in that direction 
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y, enemy.position.z);

    }

    // OnDisable() method deactivates the walking animation of the enemy
    private void OnDisable()
    {
        anim.SetBool("isWalking", false);
    }
}
