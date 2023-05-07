using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePop : MonoBehaviour
{
    public GameObject Message;

// check if the player will trigger the message
    private void OnTriggerEnter2D(Collider2D other)
    {
      // checks if the gameobject tag  is the Player tag
         if(other.gameObject.tag =="Player")
         {
            // sets message to true
            Message.SetActive(true);
         }

    }
    //  if the player does not trigger the message will not be displayed
    private void OnTriggerExit2D(Collider2D other)
    {
       // checks if the gameobject tag  is the Player tag
         if(other.gameObject.tag =="Player")
         {
            // the message is set to false 
            Message.SetActive(false);
         }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
