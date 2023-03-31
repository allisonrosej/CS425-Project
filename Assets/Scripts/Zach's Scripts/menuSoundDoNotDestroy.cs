using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSoundDoNotDestroy : MonoBehaviour
{
    private void Awake(){

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("menuMusicTag");
        if(musicObj.Length > 1){
            Destroy(this.gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
    }
}
