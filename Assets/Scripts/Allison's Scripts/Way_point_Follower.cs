using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way_point_Follower : MonoBehaviour
{
      [SerializeField] private GameObject[] waypoints;
  
     // variable to keep track og current waypoint index
     private int currentWaypointIndex = 0;

     // variable to be able change the speed of the platform
     [SerializeField] private float speed = 2f;


    // Update is called once per frame
  private  void Update()
    {
        if( Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < .1f)
        {
            currentWaypointIndex++;
            if(currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position,waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
