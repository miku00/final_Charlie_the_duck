using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelToLoad;
    private LevelManager levelManager;

    // Use this for initialization
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            PlayerPrefs.SetInt("CoinCount", levelManager.coinCount);
            PlayerPrefs.SetInt("CurrentLives", levelManager.currentLives);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}

