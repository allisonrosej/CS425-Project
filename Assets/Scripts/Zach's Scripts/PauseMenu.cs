using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){

            if(isGamePaused){
                Resume();
            }

            else{
                Pause();
            }
        }

    }

    public void pauseMainMenuButton(){
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void pauseExitGameButton(){
        Application.Quit();
        Debug.Log("Quitting Game...");
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        
    }

    void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        
    }
}
