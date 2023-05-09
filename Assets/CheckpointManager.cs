using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Checkpoint Manager Settings: ")]
    public Transform playerSpawnPoint;
    public Transform playerTransform; 
    public Transform lastCheckpoint;
    private Health playerHealth;
    private Player_Movement playerMovement;
    public UIManager UIManager;
 
    private void Start()
    {
        // starts checkpoint to players spawn point
        lastCheckpoint = playerSpawnPoint;
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerMovement = GameObject.Find("Player").GetComponent<Player_Movement>();
        
    }

    // Respawns the player using the last checkpoint position. Calls the functions to 
    // reset the health, reenables playermovement and deactivates the death panel. 
    public void Respawn()
    {
        playerTransform.position = lastCheckpoint.position;
        playerHealth.Respawn();
        playerMovement.Respawn();
        LevelManager.instance.Respawn();
        //UIManager.ToggleDeathPanel();

    }
}
