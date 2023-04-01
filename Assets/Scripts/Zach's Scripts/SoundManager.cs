using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class SoundManager : MonoBehaviour
{
    public AudioSource footstepsSound;
    public AudioSource jumpSound;
    public AudioSource meleeSound;
    public AudioSource projectileAttackSound;
    public AudioSource takeDamageSound;
    public AudioSource dashSound;
    public AudioSource mushroomCollectSound;
    public AudioSource buttonHoverSound;
    public AudioSource buttonClickSound;
    public Player_Movement player; 

    void Update()
    {
        //int jumpCounter = 0;

        if (player.horizontalDir != 0 && player.canMove && !player.isInAir)
        {
            footstepsSound.enabled = true;
        }
        else
        {
            footstepsSound.enabled = false;
        }

        
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    jumpSound.Play();
        //}


        

    }
}
