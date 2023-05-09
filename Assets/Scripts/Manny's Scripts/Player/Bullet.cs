using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings: ")]
    public float speed;
    public CircleCollider2D cirCollider;
    public Animator animator;
    public bool hit;
    private float direction;
    public Rigidbody2D rb;
    private EnemyHealth enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        cirCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
            return;

        //rb.velocity = transform.right * speed;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

    }

    // checks if the bullet has collided with enemy and gets enemies health script and causes damage.
    // Then triggers the explode animation of the bullet.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        // if bullet hits enemy then cause damage to the enemy
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(1);
        }


        cirCollider.enabled = false;
        animator.SetTrigger("Explode");
    }

    // SetDirection() method give a direction send the bullet in that dirrection.
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        cirCollider.enabled = true;

        // float localScaleX = transform.localScale.x;

        // if (Mathf.Sign(localScaleX) != _direction)
        //   transform.rotation = new Quaternion(0, 180, 0, 0);

        // transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    // Deactivate() method deactives the bullet in the explosion animation
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
