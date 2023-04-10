using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject DeathPanel;

    public void ToggleDeathPanel()
    {
        DeathPanel.SetActive(!DeathPanel.activeSelf);
        
    }

    public void ActiveDeathPanel()
    {
        DeathPanel.SetActive(true);
    }

    public void DeactiveDeathPanel()
    {
        DeathPanel.SetActive(false);
    }
}
