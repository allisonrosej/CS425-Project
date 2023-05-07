using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Way_point_Follower : MonoBehaviour
{
    //  varaible for keeping track of the waypoints
      [SerializeField] private GameObject[] waypoints;
  
     // variable to keep track og current waypoint index
     private int currentWaypointIndex = 0;

     // variable to be able change the speed of the platform
     [SerializeField] private float speed = 2f;


    // Update is called once per frame
  private  void Update()
    {
        // checks the distance between the platforms and the currrently active  waypoint  to the next way point
        if( Vector2.Distance(waypoints[currentWaypointIndex].transform.position,transform.position) < .1f)
        {  
            // the current way point is incremented
            currentWaypointIndex++;
            //  when we are at the last index we switch back to the first one ( cycle between way points)
            if(currentWaypointIndex >= waypoints.Length)
            {
                // sets the current way point to zero 
                currentWaypointIndex = 0;
            }
        }
        // moves platform frame by frame
        transform.position = Vector2.MoveTowards(transform.position,waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
