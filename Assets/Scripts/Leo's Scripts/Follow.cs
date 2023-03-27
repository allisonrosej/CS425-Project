using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public GameObject target;
    public float ratio = 0.05f;
    bool foundPlayer;
    public float range;

    // Start is called before the first frame update
    void Start()
    {

        if(target == null)
            target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {


        if (!foundPlayer)
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range);

            foreach (Collider2D h in hits)
                if (h.gameObject == target)
                {
                    foundPlayer = true;

                    break;
                }

        }


        if (foundPlayer)
        {

            // remove = ratio if uncommenting code below
            float step = ratio;

            //if (Vector3.Dot(target.transform.position - transform.position, target.transform.localScale.x * Vector3.right) > 0)
            //{
             //   step = ratio * 2;
           // }
            //else
            //{
               // step = ratio;
            //}

            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, range);
    }

}
