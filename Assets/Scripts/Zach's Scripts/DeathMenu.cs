using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void deathExitGameButton()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }

    public void optionsButton()
    {
        SceneManager.LoadScene("Options Menu");
    }

    public void deathBackButton()
    {
        //SceneManager.LoadScene("Main Menu");
    }

    public void deathMainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");
        GameObject.FindGameObjectWithTag("menuMusicTag").GetComponent<menuSoundDoNotDestroy>().GetComponent<AudioSource>().Play(); //starts menu music
    }
}
