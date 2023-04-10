using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MushroomDisplay : MonoBehaviour
{
    public Image[] mushroom;
    public Collectibles collects; 

    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mushroom.Length; i++)
        {
            mushroom[i].gameObject.SetActive(false);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collects.mushroomCount == 1)
        {
            mushroom[0].gameObject.SetActive(true);
        }
        else if(collects.mushroomCount == 2)
        {
            mushroom[1].gameObject.SetActive(true);
        }
        else if (collects.mushroomCount == 3)
        {
            mushroom[2].gameObject.SetActive(true);
        }
    }
}
