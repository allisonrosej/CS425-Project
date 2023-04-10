using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void playButton()
    {
        GameObject.FindGameObjectWithTag("menuMusicTag").GetComponent<menuSoundDoNotDestroy>().GetComponent<AudioSource>().Stop(); //stops menu music
        SceneManager.LoadScene("Level_1");

    }

    public void optionsButton()
    {
        SceneManager.LoadScene("Options Menu");
    }
}
