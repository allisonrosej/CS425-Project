using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Collectibles : MonoBehaviour
{
    [Header("Collectibles Settings:")]
    public int mushroomCount = 0;
    public int coinCount = 0;
    public Health player;
    public TextMeshProUGUI coinText;
    private Animator anim;
    public AudioSource coinCollectSound;
    public AudioSource mushroomCollectSound;
    public AudioSource heartCollectSound;

    private void Start()
    {
        SetCountText();
        coinCount = 0;
        mushroomCount = 0;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mushroomCount += 1;
        }

        // Win condition
        if (mushroomCount >= 3)
        {
            SceneManager.LoadScene("Win");
        }
    }

    // Keeps count of the mushrooms, coins, and hearts when it collides with those items
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // mushroom count
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            mushroomCount += 1;
            anim.SetTrigger("Dance");
            collision.gameObject.SetActive(false);
            mushroomCollectSound.Play();

            

        }
        // coin count
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;
            collision.gameObject.SetActive(false);
            SetCountText();
            coinCollectSound.Play();

        }
        // Heart count
        if (collision.gameObject.CompareTag("Heart"))
        {
            // Increase health by one heart
            player.health += 1;
            collision.gameObject.SetActive(false);
            heartCollectSound.Play();
        }


    }

    // SetCountTest() method sets the count of the coins to the text mesh pro of the UI
    void SetCountText()
    {
        coinText.text = coinCount.ToString();
    }
}
