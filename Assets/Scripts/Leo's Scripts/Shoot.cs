using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private Animator anim;
    private GameObject player;
    public Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 19)
        {
            timer += Time.deltaTime;





            if (timer > 1 && !playerHealth.playerdead)
            {



                timer = 0;


                  
                shoot();
            }

        }

    }

    void shoot()
    {
        anim.SetTrigger("Attack3");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }



}
