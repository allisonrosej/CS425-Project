using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning_platforms : MonoBehaviour
{

    private Rigidbody2D rbd;
    private BoxCollider2D boxcollider;

    public float fallplat;

    public float fallplaton;

    public bool isFalling = false;

    public Vector2 initialposition;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        initialposition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallplat);
        }
    }

    void Fall()
    {
        rbd.isKinematic = false;
        boxcollider.isTrigger = true;
        isFalling = true;
    }

    void respawn()
    {
        StartCoroutine(respawnco());
    }

    IEnumerator respawnco()
    {
        yield return new WaitForSeconds(fallplaton);
        isFalling = false;
        rbd.isKinematic = true;
        boxcollider.isTrigger = false;
        transform.position = initialposition;
        rbd.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")

        {
            rbd.isKinematic = true;
            boxcollider.isTrigger = false;
            isFalling = false;
            respawn();
        }
    }
}

