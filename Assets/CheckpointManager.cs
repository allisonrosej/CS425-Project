using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform playerTransform; 
    public Transform lastCheckpoint;
    private Health playerHealth;
    private Player_Movement playerMovement;
    public UIManager UIManager;

    private void Start()
    {
        lastCheckpoint = playerSpawnPoint;
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerMovement = GameObject.Find("Player").GetComponent<Player_Movement>();
    }

    public void Respawn()
    {
        playerTransform.position = lastCheckpoint.position;
        playerHealth.Respawn();
        playerMovement.Respawn();
        UIManager.ToggleDeathPanel();

    }
}
