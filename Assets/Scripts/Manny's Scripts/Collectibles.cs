using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Collectibles : MonoBehaviour
{

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

        if (mushroomCount >= 3)
        {
            SceneManager.LoadScene("Win");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom"))
        {
            mushroomCount += 1;
            anim.SetTrigger("Dance");
            collision.gameObject.SetActive(false);
            mushroomCollectSound.Play();

            

        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount += 1;
            collision.gameObject.SetActive(false);
            SetCountText();
            coinCollectSound.Play();

        }

        if (collision.gameObject.CompareTag("Heart"))
        {
            player.health += 1;
            collision.gameObject.SetActive(false);
            heartCollectSound.Play();
        }


    }

    void SetCountText()
    {
        coinText.text = coinCount.ToString();
    }
}
