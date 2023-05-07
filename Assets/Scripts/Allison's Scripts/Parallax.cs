using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
   // variables 
   private float length, startpos;
   public GameObject cam;
   public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        // sets the transform.position.x to startpos
        startpos = transform.position.x;
        // gets the length 
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
       // sets the float distance 
        float dist = (cam.transform.position.x * parallaxEffect);
         //
             transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}

