using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void optionsBackButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void optionsControlsButton()
    {
        SceneManager.LoadScene("Controls Menu");
    }

    public void controlsBackButton()
    {
        SceneManager.LoadScene("Options Menu");
    }
}
