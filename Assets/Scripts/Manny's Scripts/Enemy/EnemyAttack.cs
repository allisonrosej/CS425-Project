using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy Attack Settings:")]
    public float attackCoolDown;
    public float range;
    public float distance; 
    public int damage; 
    private float coolDownTimer = Mathf.Infinity;
    public LayerMask playerLayer;
    public CircleCollider2D cirCol;
    private Animator anim;
    public Health playerHealth;
    private EnemyScout enemyScout;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyScout = GetComponentInParent<EnemyScout>();
        
    }
    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            // Only attack if the player is not dead and the cool down timer is >= attack cool down
            if (coolDownTimer >= attackCoolDown && !playerHealth.playerdead)
            {
                coolDownTimer = 0;
                anim.SetTrigger("Attack");

            }
        }

        // if player is not in sight then scout the area
        if (enemyScout != null)
            enemyScout.enabled = !PlayerInSight();
       
    }

    // PlayerInSight() method creates a box raycasting and assigns the playerhealth when it collides to a player
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(cirCol.bounds.center + transform.right * range * -transform.localScale.x * distance, new Vector3(cirCol.bounds.size.x * range, cirCol.bounds.size.y, cirCol.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        // Checks if ray hit something and trys to get the playerhealth from the player 
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        // returns true if something is in the range
        return hit.collider != null;
    }

    // OnDrawGizmos method draws the raycasting box
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cirCol.bounds.center + transform.right * range * -transform.localScale.x * distance, new Vector3(cirCol.bounds.size.x * range, cirCol.bounds.size.y, cirCol.bounds.size.z));
    }

    // DamagePlayer() Method is called in the animation of the enemy and attack the player at a certain frame
    private void DamagePlayer()
    {
        // if player is in the raycasting box then cause damage
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
