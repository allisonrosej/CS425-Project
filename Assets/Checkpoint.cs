using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager checkpointManager; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mushroom")
        {
            checkpointManager.lastCheckpoint = collision.transform;

            Debug.Log("Checkpoint Reached!");
        }
    }
   
}
