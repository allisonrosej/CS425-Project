using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public CheckpointManager checkpointManager;
    public Transform bossSpawnPoint;
    public Transform playerTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerTransform.position = bossSpawnPoint.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Mushroom")
        {
            checkpointManager.lastCheckpoint = collision.transform;

            Debug.Log("Checkpoint Reached!");
        }

        if (collision.tag == "BossSpawn")
        {
            checkpointManager.lastCheckpoint = collision.transform;

            Debug.Log("Boss Checkpoint Reached!");
        }

        
    }
   
}
