using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public float timeUntilRespawn; // A halál és újraéledés közötti kis idő
    public float timeAfterDeath = 10; 
    public PlayerController player;

    public GameObject AnimOnDeath;

    public int coinCount;

    public Text coinsText;

    //Sounds
    public AudioSource coinSound;
    

    public int actualHealth;
    public int maxHealth;

    public Image gem1;
    public Image gem2;
    public Image gem3;
    public Image gem4;
    public Image gem5;

    public Sprite fullGem;
    public Sprite emptyGem;

    private bool respawning;

    public ResetOnRespawn[] objectsToReset;

    public bool invincible;

    public GameObject gameOverScreen;
    public GameObject congratulationScreen;

    public Text livesText;
    public int startingLives;
    public int currentLives;


    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        actualHealth = maxHealth;
        coinCount = 0;
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();

        if (PlayerPrefs.HasKey("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }

        coinsText.text = "Coins: " + coinCount;

        if (PlayerPrefs.HasKey("CurrentLives"))
        {
            currentLives = PlayerPrefs.GetInt("CurrentLives");
        }
        else
        {
            currentLives = startingLives;
        }       
        
        livesText.text = "Lives x " + currentLives;

    }

    // Update is called once per frame
    void Update()
    {
        if (actualHealth <= 0 && !respawning)
        {
            Respawn();
            respawning = true;
        }
        
        if(coinCount >= 20)
        {
            congratulationScreen.SetActive(true);
            StartCoroutine("OnRestartGame");
        }

    }

    public void Respawn()
    {
        currentLives -= 1;

        livesText.text = "Lives x " + currentLives;

        if (currentLives > 0)
        {
            StartCoroutine("RespawnCo");
        }
        else
        {
            player.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);

            StartCoroutine("OnRestartGame");
        }
    }

    private IEnumerator OnRestartGame()
    {
        yield return new WaitForSeconds(timeAfterDeath);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);

        Instantiate(AnimOnDeath, player.transform.position, player.transform.rotation);

        yield return new WaitForSeconds(timeUntilRespawn);

        /*
         * Vizsgálom, hogy esés miatt kell-e újraéledni, és hogy van-e
         még élete, különben a teljes újraéledés és reset következik be.
         */

        actualHealth = maxHealth;

        respawning = false;

        coinCount = 0;
        coinsText.text = "Coins: " + coinCount;

        UpdateHealthMeter();
        player.transform.position = player.respawnPosition;
        player.gameObject.SetActive(true);


        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].gameObject.SetActive(true);
            objectsToReset[i].ResetObject();
        }

    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinsText.text = "Coins: " + coinCount;

        coinSound.Play();
    }

    public void HurtPlayer(int dmg)
    {
        if (!invincible)
        {
            actualHealth -= dmg;
            UpdateHealthMeter();

            player.KnockBack();

            player.hurtSound.Play();
        }
    }

    public void AddLives(int lives)
    {
        currentLives += lives;
        livesText.text = "Lives x " + currentLives;
    }

    public void UpdateHealthMeter()
    {
        switch (actualHealth)
        {
            case 5:
                gem1.sprite = fullGem;
                gem2.sprite = fullGem;
                gem3.sprite = fullGem;
                gem4.sprite = fullGem;
                gem5.sprite = fullGem;
                return;
            case 4:
                gem1.sprite = fullGem;
                gem2.sprite = fullGem;
                gem3.sprite = fullGem;
                gem4.sprite = fullGem;
                gem5.sprite = emptyGem;
                return;
            case 3:
                gem1.sprite = fullGem;
                gem2.sprite = fullGem;
                gem3.sprite = fullGem;
                gem4.sprite = emptyGem;
                gem5.sprite = emptyGem;
                return;
            case 2:
                gem1.sprite = fullGem;
                gem2.sprite = fullGem;
                gem3.sprite = emptyGem;
                gem4.sprite = emptyGem;
                gem5.sprite = emptyGem;
                return;
            case 1:
                gem1.sprite = fullGem;
                gem2.sprite = emptyGem;
                gem3.sprite = emptyGem;
                gem4.sprite = emptyGem;
                gem5.sprite = emptyGem;
                return;
            case 0:
                gem1.sprite = emptyGem;
                gem2.sprite = emptyGem;
                gem3.sprite = emptyGem;
                gem4.sprite = emptyGem;
                gem5.sprite = emptyGem;
                return;
            default:
                gem1.sprite = fullGem;
                gem2.sprite = fullGem;
                gem3.sprite = fullGem;
                gem4.sprite = emptyGem;
                gem5.sprite = emptyGem;
                return;
        }
    }
}
