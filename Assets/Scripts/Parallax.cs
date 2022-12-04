using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

private float length, startpos; 
public GameObject cam;
public float parallaxEffect;  // how much parallax we want to apply

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x; // find the start position
        length = GetComponent<SpriteRenderer>().bounds.size.x; // gets the length of sprites
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist  = (cam.transform.posistion.x * parallaxEffect); // how much we move 

        transform.position = new Vector3(startpos + dist,transform.posistion.y,transform.posistion.y); // 
    }
}
