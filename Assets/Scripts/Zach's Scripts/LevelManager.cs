using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Animator anim;
    private void Awake()
    {
       

        if (LevelManager.instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {

            _ui.ToggleDeathPanel();
        }
    }

    public void Death()
    {

        UIManager _ui = GetComponent<UIManager>();
        _ui.ActiveDeathPanel();
    }

    public void Respawn()
    {
        UIManager _ui = GetComponent<UIManager>();
        _ui.DeactiveDeathPanel();
    }

}
