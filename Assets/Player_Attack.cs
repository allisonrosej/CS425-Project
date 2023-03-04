using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.05f;
    public LayerMask enemyLayer;
    public int attackDamage = 2;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    //private string attack = "Attack";

    public Transform firePoint;

    public GameObject[] bullets;
    public float time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Attack();
                //StartCoroutine(TimeForNextAttack());
                nextAttackTime = Time.time + 1 / attackRate;

            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                StartCoroutine(Shoot());
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }




    }

    private void Attack()
    {
        // Play an attack animation 
        //Debug.Log("Attack");
        animator.SetTrigger("Melee Attack");

        // Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);




        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name); 

            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }


    }

    IEnumerator TimeForNextAttack()
    {
        yield return new WaitForSeconds(nextAttackTime);
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator Shoot()
    {
        animator.SetTrigger("Projectile Attack");
        yield return new WaitForSeconds(time);
        bullets[FindBullets()].transform.position = firePoint.position;
        bullets[FindBullets()].transform.rotation = firePoint.rotation;

        bullets[FindBullets()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));


        // Fireball object pooling 


    }

    private int FindBullets()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }

        return 0;
    }


}
