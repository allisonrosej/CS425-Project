using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashController : MonoBehaviour
{
    // Deactivate() method deactives the smoke of the dash. This method is called in the animation of the player.
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
