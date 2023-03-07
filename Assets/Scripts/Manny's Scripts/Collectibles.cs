using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Collectibles : MonoBehaviour
{

    public int mushroomCount = 0;
    public int coinCount = 0;
    public Health player;
    public TextMeshProUGUI coinText;

    private void Start()
    {
        SetCountText();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            mushroomCount += 1;
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;
            collision.gameObject.SetActive(false);
            SetCountText();
        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            player.health += 1;
            collision.gameObject.SetActive(false);
        }


    }

    void SetCountText()
    {
        coinText.text = coinCount.ToString();
    }
}
