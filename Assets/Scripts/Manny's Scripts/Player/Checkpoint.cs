using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Settings:")]
    public CheckpointManager checkpointManager;
    public Transform bossSpawnPoint;
    public Transform playerTransform;

    private void Update()
    {
        // Back door to the boss
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playerTransform.position = bossSpawnPoint.position;
        }
    }

    // Set up checkpoint when colliding with mushrooms and BossSpawn objects.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // mushrooms are the checkpoints
        if (collision.tag == "Mushroom")
        {
            checkpointManager.lastCheckpoint = collision.transform;

            Debug.Log("Checkpoint Reached!");
        }
        // set up spawn point before boss fight 
        if (collision.tag == "BossSpawn")
        {
            checkpointManager.lastCheckpoint = collision.transform;

            Debug.Log("Boss Checkpoint Reached!");
        }

        
    }
   
}
