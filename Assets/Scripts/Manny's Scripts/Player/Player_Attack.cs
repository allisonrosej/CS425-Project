using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [Header("Player Attack Settings: ")]
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.05f;
    public LayerMask enemyLayer;
    public int attackDamage = 3;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    
    //private string attack = "Attack";

    public Transform firePoint;

    public GameObject[] bullets;
    public float time = 0.5f;
    private Player_Movement player;

    public AudioSource meleeSound;
    public AudioSource projectileSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player_Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.pauseInput)
            return;
        if (player.demo)
            return; 

        // attack cool down
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

    // Attack() method creates a circle where the sword is located and gets the enemy heath
    // component and causes damage to the enemy
    private void Attack()
    {
        // Play an attack animation 
        //Debug.Log("Attack");
        animator.SetTrigger("Melee Attack");
        meleeSound.Play();
        // Detect enemies in range of attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name); 

            enemy.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }


    }

    // TimeForNextAttack() method causes a delay until the next attack
    IEnumerator TimeForNextAttack()
    {
        yield return new WaitForSeconds(nextAttackTime);
    }

    // OnDrawGizmosSelected() Draws the circle of the hit circle of the sword attack
    private void OnDrawGizmosSelected()
    {
        // if attack point not set then dont draw the circle
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Shoot() method trigers the projectile attack animation and shoots the bullet given the firepoint position and rotation
    IEnumerator Shoot()
    {
        animator.SetTrigger("Projectile Attack");
        yield return new WaitForSeconds(time);
        // Fireball object pooling 
        bullets[FindBullets()].transform.position = firePoint.position;
        bullets[FindBullets()].transform.rotation = firePoint.rotation;

        bullets[FindBullets()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));

        projectileSound.Play();
        
    }

    // FindBullets() method check in the bullet array for any bullets that are not active and returns the index
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
