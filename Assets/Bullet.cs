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

    // Start is called before the first frame update
    void Start()
    {
        cirCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
            return;

        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0); 


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true; 

        cirCollider.enabled = false;
        animator.SetTrigger("Explode");
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        cirCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
