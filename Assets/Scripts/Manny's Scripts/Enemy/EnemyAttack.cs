using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCoolDown;
    public float range;
    public float distance; 

    public int damage; 
    private float coolDownTimer = Mathf.Infinity;
    public LayerMask playerLayer;
    public CircleCollider2D cirCol;
    private Animator anim;
    private Health playerHealth;
    public EnemyScout enemyScout;
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
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("Attack");

            }
        }

        if (enemyScout != null)
            enemyScout.enabled = !PlayerInSight();
       
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(cirCol.bounds.center + transform.right * range * -transform.localScale.x * distance, new Vector3(cirCol.bounds.size.x * range, cirCol.bounds.size.y, cirCol.bounds.size.z), 0, Vector2.left, 0, playerLayer);
        
        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(cirCol.bounds.center + transform.right * range * -transform.localScale.x * distance, new Vector3(cirCol.bounds.size.x * range, cirCol.bounds.size.y, cirCol.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
