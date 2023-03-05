using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        if (collision.gameObject.tag == "Enemy")
        {
           // TestEnemy enemy;
            enemyHealth = collision.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(3);
        }


        cirCollider.enabled = false;
        animator.SetTrigger("Explode");
    }

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

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
